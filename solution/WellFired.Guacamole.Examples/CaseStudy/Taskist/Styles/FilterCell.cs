using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.Styles
{
    internal static class FilterCell
    {
        private static UIColor _lightColor = UIColor.FromRGB(255, 255, 255);
        private static UIColor _darkerColor = UIColor.FromRGB(250, 250, 250);
        
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(250, 250, 250)},
                new Setter {Property = Views.View.OutlineColorProperty, Value = UIColor.FromRGB(250, 250, 250)},
                new Setter {Property = Views.View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
                new Setter {Property = Views.View.VerticalLayoutProperty, Value = LayoutOptions.Expand}
            },
            Triggers =
            {
                new Trigger
                {
                    Property = Cells.Cell.IsSelectedProperty,
                    Value = true,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = _lightColor}
                    }
                },
                new Trigger
                {
                    Property = Cells.Cell.IsSelectedProperty,
                    Value = false,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = _darkerColor}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Hover,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = _lightColor}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Normal,
                    Conditionals = { new Conditional { Property = Cells.Cell.IsSelectedProperty, Value = false } },
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = _darkerColor}
                    }
                }
            }
        };
    }
}