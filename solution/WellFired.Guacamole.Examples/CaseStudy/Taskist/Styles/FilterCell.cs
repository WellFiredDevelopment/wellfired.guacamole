using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.Styles
{
    internal static class FilterCell
    {
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
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Normal,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(250, 250, 250)}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Hover,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(255, 255, 255)}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Active,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(255, 255, 255)}
                    }
                }
            }
        };
    }
}