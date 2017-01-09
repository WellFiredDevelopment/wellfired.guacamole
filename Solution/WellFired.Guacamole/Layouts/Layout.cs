using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Layouts
{
	public abstract class Layout : View, ICanLayout
	{
	    public IList<View> Children { get; set; }

	    public abstract void DoLayout();

	    protected Layout()
		{
		    Children = new List<View>();
		    OutlineColor = UIColor.Clear;
			Orientation = OrientationOptions.Vertical;
			HorizontalLayout = LayoutOptions.Expand;
			VerticalLayout = LayoutOptions.Expand;
		}

		public OrientationOptions Orientation { protected get; set; }
		public int Spacing { protected get; set; }

	    public override void Render(UIRect parentRect)
	    {
	        base.Render(parentRect);

	        foreach (var child in Children)
	            child.Render(FinalRenderRect);
	    }

	    public override void InvalidateRectRequest()
	    {
	        base.InvalidateRectRequest();

	        foreach (var child in Children)
	            child.InvalidateRectRequest();
	    }

	    public override void CalculateRectRequest()
	    {
	        // When calculating size, we want to recurse the whole structure, calculating the size of the Child
	        // components first of all.
	        foreach (var child in Children)
	            child.CalculateRectRequest();

	        base.CalculateRectRequest();
	    }

	    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	    {
	        base.OnPropertyChanged(sender, e);

	        if (e.PropertyName != BindingContextProperty.PropertyName)
	            return;

	        foreach (var child in Children)
	            child.BindingContext = BindingContext;
	    }
	}
}