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
                Text = "Press Me Please.",
                Style = new Style {
                    Triggers = {
                        new Trigger {
                            Property = Button.EnabledProperty,
                            Value = false,
                            Setters = {
                                new Setter {
                                    Property = BackgroundColorProperty,
                                    Value = UIColor.Aquamarine
                                }
                            }
                        },
                        new Trigger {
                            Property = Button.EnabledProperty,
                            Value = true,
                            Setters = {
                                new Setter {
                                    Property = BackgroundColorProperty,
                                    Value = UIColor.Purple
                                }
                            }
                        }
                    }
                }
            };

            var buttonPressed = new Command
            {
                ExecuteAction = () =>
                {
                    button.Enabled = !button.Enabled;
                },
                CanExecuteAction = () => true
            };

            button.Command = buttonPressed;
            button.Enabled = true;

            Content = button;
        }
    }
}