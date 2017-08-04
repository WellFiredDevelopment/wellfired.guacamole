using WellFired.Guacamole.Data;
using WellFired.Guacamole.Styling;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles
{
    internal static class ViewContainer
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.FromRGB(40, 40, 40)},
                new Setter {Property = Views.View.OutlineColorProperty, Value = UIColor.FromRGB(40, 40, 40)}
            }
        };
    }
}