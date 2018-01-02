using System.ComponentModel;
using System.Diagnostics;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole.Views
{
	public class Window : View
	{
		private readonly MainThreadRunner _mainThreadRunner = new MainThreadRunner();

		[PublicAPI]
		public Window(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider)
		{
			StyleDictionary = new StyleDictionary(logger);
		}

		[PublicAPI]
		public Window(ILogger logger, IPlatformProvider platformProvider)
		{
			StyleDictionary = new StyleDictionary(logger);
		}

		private UIRect FinalRenderedRect { get; set; }

		public void Layout(UIRect rect)
		{
		    var view = Content as View;
		    Debug.Assert(view != null, "view != null");

		    ViewSizingExtensions.DoSizingAndLayout(view, rect - Padding);
			FinalRenderedRect = rect;
		    ViewSizingExtensions.UpdateContextIfNeeded(view);
		}

		public override void Render(UIRect parentRect)
		{
		    var view = Content as View;
		    Debug.Assert(view != null, "view != null");

			_mainThreadRunner.ProcessActions();
			NativeRenderer.Render(FinalRenderedRect);

			var relativeParentRect = UIRect.With(0, 0, parentRect.Width, parentRect.Height);
		    view.Render(relativeParentRect);
		}

		protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnPropertyChanged(sender, e);

		    var view = Content as View;
		    Debug.Assert(view != null, "view != null");

		    if (e.PropertyName == BindingContextProperty.PropertyName)
			    view.BindingContext = BindingContext;
		}
		
		public void SetContent(IView content)
		{
			Content = content;
			Content.SetStyleDictionary(StyleDictionary);
		}
	}
}