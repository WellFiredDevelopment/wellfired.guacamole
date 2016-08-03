using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.TextFieldExample
{
    public class TextFieldTestWindow : Window
    {
        public TextFieldTestWindow()
        {
            BackgroundColor = UIColor.White;
            Padding = new UIPadding(5);

            Content = new TextEntry {
                VerticalTextAlign = UITextAlign.Middle,
                BackgroundColor = UIColor.Beige,
                Text = "Test"
            };
        }
    }
}