using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
	public abstract class Layout : View, ICanLayout
	{
	    public IList<ILayoutable> Children { get; set; }

	    public abstract void DoLayout();

	    protected Layout()
		{
		    Children = new List<ILayoutable>();
		    OutlineColor = UIColor.Clear;
			HorizontalLayout = LayoutOptions.Expand;
			VerticalLayout = LayoutOptions.Expand;
		}

		public int Spacing { protected get; set; }

	    public override void Render(UIRect parentRect)
	    {
	        base.Render(parentRect);

	        foreach (var child in Children)
	            (child as View)?.Render(FinalRenderRect);
	    }

	    public override void InvalidateRectRequest()
	    {
	        base.InvalidateRectRequest();

	        foreach (var child in Children)
	            (child as View)?.InvalidateRectRequest();
	    }

	    public override void CalculateRectRequest()
	    {
	        // When calculating size, we want to recurse the whole structure, calculating the size of the Child
	        // components first of all.
	        foreach (var child in Children)
	            (child as View)?.CalculateRectRequest();

	        base.CalculateRectRequest();
	    }

	    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	    {
	        base.OnPropertyChanged(sender, e);

	        if (e.PropertyName != BindingContextProperty.PropertyName)
	            return;

	        foreach (var child in Children)
	        {
	            var view = child as View;
	            if (view != null)
	                view.BindingContext = BindingContext;
	        }
	    }

	    public override void UpdateContextIfNeeded()
	    {
	        base.UpdateContextIfNeeded();

	        foreach(var child in Children)
	            (child as View)?.UpdateContextIfNeeded();
	    }
	}
}