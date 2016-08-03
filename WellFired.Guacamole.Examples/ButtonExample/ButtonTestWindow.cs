using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.ButtonExample
{
    public class ButtonTestWindow : Window
    {
        public ButtonTestWindow()
        {
            Padding = new UIPadding(5);

            Content = new Button {
                Text = "Press Me Please."
            };
        }
    }
}