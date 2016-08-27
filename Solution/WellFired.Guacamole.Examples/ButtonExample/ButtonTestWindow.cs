using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.ButtonExample
{
    public class ButtonTestWindow : Window
    {
        public ButtonTestWindow()
        {
            Padding = new UIPadding(5);

            var button = new Button
            {
                Text = "Press Me Please."
            };
			
            var buttonPressed = new Command
            {
                ExecuteAction = () =>
                {
					Logger.LogMessage("Sausages");
                },
                CanExecuteAction = () => true
			};

            button.Command = buttonPressed;

			Content = button;
		}
	}
}