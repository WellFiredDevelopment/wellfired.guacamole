using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class LabelCell
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGBA(125, 125, 125, 255)},
                new Setter {Property = View.OutlineColorProperty, Value = UIColor.FromRGB(88, 88, 88)},
            },
            Triggers =
            {
            }
        };
    }
}