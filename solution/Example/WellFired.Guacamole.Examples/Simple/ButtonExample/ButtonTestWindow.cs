using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ButtonExample
{
	public class ButtonTestWindow : Window
	{
		public ButtonTestWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
		{
			Padding = UIPadding.Of(5);

			var button = new Button {
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