using WellFired.Guacamole.Data;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles
{
	public class OrderableColumnHeader
	{
		public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGBA(125, 125, 125, 255)},
                new Setter {Property = Views.View.OutlineColorProperty, Value = UIColor.FromRGB(88, 88, 88)},
                new Setter {Property = Views.View.OutlineThicknessProperty, Value = 4.0},
                new Setter {Property = Views.View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
                new Setter {Property = Views.View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
                new Setter {Property = Views.View.PaddingProperty, Value = 5},
                new Setter {Property = Views.Button.TextColorProperty, Value = UIColor.White},
                new Setter {Property = Views.View.CornerRadiusProperty, Value = 0.0},
                new Setter {Property = Views.Button.HorizontalTextAlignProperty, Value = UITextAlign.Middle},
                new Setter {Property = Views.Button.VerticalTextAlignProperty, Value = UITextAlign.Middle}
            },
            Triggers =
            {
                new Trigger
                {
                    Property = Views.TabbedPageButton.IsSelectedProperty,
                    Value = true,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(140, 140, 140)}
                    }
                },
                new Trigger
                {
                    Property = Views.TabbedPageButton.IsSelectedProperty,
                    Value = false,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(125, 125, 125)}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Hover,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(158, 158, 158)}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Normal,
                    Conditionals = {new Conditional {Property = Views.TabbedPageButton.IsSelectedProperty, Value = false}},
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(125, 125, 125)}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Normal,
                    Conditionals = {new Conditional {Property = Views.TabbedPageButton.IsSelectedProperty, Value = true}},
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(140, 140, 140)}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Active,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(64, 124, 191)}
                    }
                },
                new Trigger
                {
                    Property = Views.View.ControlStateProperty,
                    Value = ControlState.Disabled,
                    Setters =
                    {
                        new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(100, 100, 100)}
                    }
                }
            }
        };
	}
}