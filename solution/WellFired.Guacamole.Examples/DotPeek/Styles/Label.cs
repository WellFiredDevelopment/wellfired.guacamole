using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.DotPeek.Styles
{
    internal static class Label
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.PaddingProperty, Value = 5},
                new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(40, 40, 40)},
                new Setter {Property = View.OutlineColorProperty, Value = UIColor.FromRGB(40, 40, 40)},
                new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
                new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
                new Setter {Property = Views.Label.TextColorProperty, Value = UIColor.White},
                new Setter {Property = Views.Label.HorizontalTextAlignProperty, Value = UITextAlign.Middle},
                new Setter {Property = Views.Label.VerticalTextAlignProperty, Value = UITextAlign.Middle}
            }
        };
    }
}