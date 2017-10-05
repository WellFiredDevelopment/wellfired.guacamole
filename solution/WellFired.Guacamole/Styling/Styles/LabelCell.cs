using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class LabelCell
    {
        private static readonly UIColor LightColor = UIColor.FromRGB(255, 255, 255);
        private static readonly UIColor DarkerColor = UIColor.FromRGB(250, 250, 250);

        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.BackgroundColorProperty, Value = DarkerColor},
                new Setter {Property = View.OutlineColorProperty, Value = DarkerColor},
                new Setter {Property = View.OutlineThicknessProperty, Value = 0.0},
            },
            Triggers =
            {
                new Trigger
                {
                    Property = Cells.Cell.IsSelectedProperty,
                    Value = true,
                    Setters =
                    {
                        new Setter {Property = View.BackgroundColorProperty, Value = LightColor}
                    }
                },
                new Trigger
                {
                    Property = Cells.Cell.IsSelectedProperty,
                    Value = false,
                    Setters =
                    {
                        new Setter {Property = View.BackgroundColorProperty, Value = DarkerColor}
                    }
                },
                new Trigger
                {
                    Property = View.ControlStateProperty,
                    Value = ControlState.Hover,
                    Setters =
                    {
                        new Setter {Property = View.BackgroundColorProperty, Value = LightColor}
                    }
                },
                new Trigger
                {
                    Property = View.ControlStateProperty,
                    Value = ControlState.Normal,
                    Conditionals = {new Conditional {Property = Cells.Cell.IsSelectedProperty, Value = false}},
                    Setters =
                    {
                        new Setter {Property = View.BackgroundColorProperty, Value = DarkerColor}
                    }
                }
            }
        };
    }
}