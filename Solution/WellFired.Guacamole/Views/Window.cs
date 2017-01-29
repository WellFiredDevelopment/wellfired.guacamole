using System.ComponentModel;
using System.Diagnostics;
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
		    var view = (Content as View);
		    Debug.Assert(view != null, "view != null");

		    ViewSizingExtensions.DosizingAndLayout(view, rect - Padding);
			FinalRenderedRect = rect;
		    ViewSizingExtensions.UpdateContextIfNeeded(view);
		}

		public override void Render(UIRect parentRect)
		{
		    var view = (Content as View);
		    Debug.Assert(view != null, "view != null");

		    _device.ProcessActions();
			NativeRenderer.Render(FinalRenderedRect);

			var relativeParentRect = UIRect.With(0, 0, parentRect.Width, parentRect.Height);
		    view.Render(relativeParentRect);
		}

		protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

		    var view = (Content as View);
		    Debug.Assert(view != null, "view != null");

		    if (e.PropertyName == BindingContextProperty.PropertyName)
			    view.BindingContext = BindingContext;
		}

	    public void RaiseEventFor(string controlId, IEvent raisedEvent)
	    {
	        throw new System.NotImplementedException();
	    }
	}
}