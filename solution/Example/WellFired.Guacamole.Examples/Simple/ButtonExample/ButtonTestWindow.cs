using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ButtonExample
{
	public class ButtonTestWindow : Window
	{
		public ButtonTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			var button = new ButtonView {
				Text = "Press Me Please."
			};

			var buttonPressed = new Command {
				ExecuteAction = () => { Logger.LogMessage("Button Click Command"); }
			};

			button.ButtonPressedCommand = buttonPressed;

			Content = button;
		}
	}
}