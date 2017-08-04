using WellFired.Guacamole.Data;
using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles
{
    internal static class ColumnLegendButton
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(10, 10, 10)},
                new Setter {Property = Views.View.OutlineColorProperty, Value = UIColor.FromRGB(40, 40, 40)},
                new Setter {Property = Views.Label.TextColorProperty, Value = UIColor.White},
                new Setter {Property = Views.Label.VerticalTextAlignProperty, Value = UITextAlign.Middle},
                new Setter {Property = Views.View.PaddingProperty, Value = UIPadding.With(0, 5, 0, 5)}
            }
        };
    }
}