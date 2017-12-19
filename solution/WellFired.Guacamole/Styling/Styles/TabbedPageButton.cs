using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class TabbedPageButton
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(245, 245, 245)},
                new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
                new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
                new Setter {Property = View.PaddingProperty, Value = 5},
                new Setter {Property = Views.Button.TextColorProperty, Value = UIColor.White},
                new Setter {Property = View.CornerRadiusProperty, Value = 0.0},
                new Setter {Property = Views.Button.HorizontalTextAlignProperty, Value = UITextAlign.Middle},
                new Setter {Property = Views.Button.VerticalTextAlignProperty, Value = UITextAlign.Middle},
                new Setter {Property = Views.Button.TextColorProperty, Value = UIColor.Black},
                new Setter {Property = View.OutlineMaskProperty, Value = OutlineMask.Bottom},
                new Setter {Property = View.OutlineThicknessProperty, Value = 1},
            },
            Triggers =
            {
                new Trigger
                {
                    Property = Views.TabbedPageButton.IsSelectedProperty,
                    Value = true,
                    Setters =
                    {
                        new Setter {Property = View.OutlineMaskProperty, Value = OutlineMask.Left | OutlineMask.Top | OutlineMask.Right},
                        new Setter {Property = View.OutlineThicknessProperty, Value = 1},
                        new Setter {Property = View.BackgroundColorProperty, Value = UIColor.White}
                    }
                },
                new Trigger
                {
                    Property = Views.TabbedPageButton.IsSelectedProperty,
                    Value = false,
                    Setters =
                    {
                        new Setter {Property = View.OutlineMaskProperty, Value = OutlineMask.Bottom},
                        new Setter {Property = View.OutlineThicknessProperty, Value = 1},
                        new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(245, 245, 245)}
                    }
                },
                new Trigger
                {
                    Property = View.ControlStateProperty,
                    Value = ControlState.Hover,
                    Conditionals = {new Conditional {Property = Views.TabbedPageButton.IsSelectedProperty, Value = true}},
                    Setters =
                    {
                        new Setter {Property = View.OutlineMaskProperty, Value = OutlineMask.Left | OutlineMask.Top | OutlineMask.Right},
                        new Setter {Property = View.OutlineThicknessProperty, Value = 1},
                        new Setter {Property = View.BackgroundColorProperty, Value = UIColor.White}
                    }
                },
                new Trigger
                {
                    Property = View.ControlStateProperty,
                    Value = ControlState.Hover,
                    Conditionals = {new Conditional {Property = Views.TabbedPageButton.IsSelectedProperty, Value = false}},
                    Setters =
                    {
                        new Setter {Property = View.OutlineMaskProperty, Value = OutlineMask.Bottom},
                        new Setter {Property = View.OutlineThicknessProperty, Value = 1},
                        new Setter {Property = View.BackgroundColorProperty, Value = UIColor.White}
                    }
                },
                new Trigger
                {
                    Property = View.ControlStateProperty,
                    Value = ControlState.Normal,
                    Conditionals = {new Conditional {Property = Views.TabbedPageButton.IsSelectedProperty, Value = false}},
                    Setters =
                    {
                        new Setter {Property = View.OutlineMaskProperty, Value = OutlineMask.Bottom},
                        new Setter {Property = View.OutlineThicknessProperty, Value = 1},
                        new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(245, 245, 245)}
                    }
                },
                new Trigger
                {
                    Property = View.ControlStateProperty,
                    Value = ControlState.Normal,
                    Conditionals = {new Conditional {Property = Views.TabbedPageButton.IsSelectedProperty, Value = true}},
                    Setters =
                    {
                        new Setter {Property = View.OutlineMaskProperty, Value = OutlineMask.Left | OutlineMask.Top | OutlineMask.Right},
                        new Setter {Property = View.OutlineThicknessProperty, Value = 1},
                        new Setter {Property = View.BackgroundColorProperty, Value = UIColor.White}
                    }
                },
                new Trigger
                {
                    Property = View.ControlStateProperty,
                    Value = ControlState.Active,
                    Setters =
                    {
                        new Setter {Property = View.OutlineMaskProperty, Value = OutlineMask.Left | OutlineMask.Top | OutlineMask.Right},
                        new Setter {Property = View.OutlineThicknessProperty, Value = 1},
                        new Setter {Property = View.BackgroundColorProperty, Value = UIColor.White}
                    }
                },
                new Trigger
                {
                    Property = View.ControlStateProperty,
                    Value = ControlState.Disabled,
                    Setters =
                    {
                        new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(100, 100, 100)}
                    }
                }
            }
        };
    }
}