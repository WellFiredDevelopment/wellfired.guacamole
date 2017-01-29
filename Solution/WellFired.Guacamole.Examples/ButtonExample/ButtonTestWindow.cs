using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.ButtonExample
{
	public class ButtonTestWindow : Window
	{
		public ButtonTestWindow()
		{
			Padding = UIPadding.Of(5);

			var button = new Button
			{
				Text = "Press Me Please."
			};

			var buttonPressed = new Command
			{
				ExecuteAction = () => { Logger.LogMessage("Sausages"); },
				CanExecuteAction = () => true
			};

			button.ButtonPressedCommand = buttonPressed;

			Content = button;
		}
	}
}