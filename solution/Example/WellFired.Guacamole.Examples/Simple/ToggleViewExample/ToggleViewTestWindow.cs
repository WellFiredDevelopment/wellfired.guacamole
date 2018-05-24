using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ToggleViewExample
{
	public class ToggleViewTestWindow : Window
	{
		public ToggleViewTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			var toggleView = new ToggleView();

			var buttonPressed = new Command {
				ExecuteAction = () => { Logger.LogMessage("Button Click Command"); }
			};

			toggleView.ButtonPressedCommand = buttonPressed;

			Content = toggleView;
		}
	}
}