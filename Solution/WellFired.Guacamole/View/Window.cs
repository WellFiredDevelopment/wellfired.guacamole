using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
	public class Window : ViewBase
	{
	    private UIRect FinalRenderedRect { get; set; }
	    protected ViewBase Content { private get; set; }
		
		public void Layout(UIRect rect)
		{
			Content.CalculateRectRequest();
			Content.AttemptToFullfillRequests(rect - Padding);

			FinalRenderedRect = rect;
			Content.Layout();
		}

		public override void Render(UIRect parentRect)
		{
			NativeRenderer.Render(renderRect : FinalRenderedRect);

			var relativeParentRect = new UIRect(0, 0, parentRect.Width, parentRect.Height);
			relativeParentRect -= Padding;
			Content.Render(parentRect: relativeParentRect);
		}

	    protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			if(e.PropertyName == BindingContextProperty.PropertyName)
				Content.BindingContext = BindingContext;
		}

        [PublicAPI]
	    public Window(INotifyPropertyChanged persistantData)
	    {
	        
	    }

        [PublicAPI]
        public Window()
	    {
	        
	    }
	}
}