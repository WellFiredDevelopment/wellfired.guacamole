using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
	public partial class LayoutView : View, ICanLayout
	{
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

		    var finalContentRect = FinalRenderRect;
		    finalContentRect.X += ContentRectRequest.X;
		    finalContentRect.Y += ContentRectRequest.Y;
		    finalContentRect.Width = ContentRectRequest.Width;
		    finalContentRect.Height = ContentRectRequest.Height;

	        foreach (var child in Children)
	            (child as View)?.Render(finalContentRect);
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

		    if (e.PropertyName == ChildrenProperty.PropertyName || 
		        e.PropertyName == BindingContextProperty.PropertyName)
			    SetupChildBindingContext();
	    }

		private void SetupChildBindingContext()
		{
	        foreach (var child in Children)
	        {
	            var view = child as View;
	            if (view != null)
	                view.BindingContext = BindingContext;
	        }
		}

		public override void SetStyleDictionary(IStyleDictionary styleDictionary)
		{
			base.SetStyleDictionary(styleDictionary);

			foreach (var child in Children)
			{
				var view = child as View;
				view?.SetStyleDictionary(styleDictionary);
			}
		}
	}
}