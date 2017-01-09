using System.ComponentModel;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Event;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
	public class Window : View
	{
		private readonly Device _device = new Device();

		[PublicAPI]
		public Window(INotifyPropertyChanged persistantData)
		{
		}

		[PublicAPI]
		public Window()
		{
		}

		private UIRect FinalRenderedRect { get; set; }

		public void Layout(UIRect rect)
		{
			(Content as View).CalculateRectRequest();
		    (Content as View).AttemptToFullfillRequests(rect - Padding);

			FinalRenderedRect = rect;
		    (Content as View).UpdateContextIfNeeded();
		}

		public override void Render(UIRect parentRect)
		{
			_device.ProcessActions();
			NativeRenderer.Render(FinalRenderedRect);

			var relativeParentRect = new UIRect(0, 0, parentRect.Width, parentRect.Height);
			relativeParentRect -= Padding;
		    (Content as View).Render(relativeParentRect);
		}

		protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

			if (e.PropertyName == BindingContextProperty.PropertyName)
			    (Content as View).BindingContext = BindingContext;
		}

	    public void RaiseEventFor(string controlId, IEvent raisedEvent)
	    {
	        throw new System.NotImplementedException();
	    }
	}
}