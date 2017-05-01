using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
	public class LayoutView : View, ICanLayout
	{
	    public IList<ILayoutable> Children { get; set; }
	    public int Spacing { [PublicAPI] get; set; }
	    public ILayoutChildren Layout { get; set; }

	    public LayoutView()
		{
		    Children = new List<ILayoutable>();
		    OutlineColor = UIColor.Clear;
			HorizontalLayout = LayoutOptions.Expand;
			VerticalLayout = LayoutOptions.Expand;
		}

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
	}
}