using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.TextFieldExample
{
    public class TextFieldTestWindow : Window
    {
        public TextFieldTestWindow()
        {
            Padding = new UIPadding(5);

            Content = new TextEntry {
                Text = "Test"
            };
        }
    }
}