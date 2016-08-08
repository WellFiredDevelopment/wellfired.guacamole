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
                Text = "Press Me Please.",
                ButtonPressed = new Command
                {
                    ExecuteAction = () => { UnityEngine.Debug.Log("Sausages"); }
                },
                Style = new Style
                {
                    Setters =
                    {
                        new Setter
                        {
                            Property = Button.TextColorProperty,
                            Value = UIColor.Black
                        }
                    }
                }
            };


            Content = button;
        }
    }
}