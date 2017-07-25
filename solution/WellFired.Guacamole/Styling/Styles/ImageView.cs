using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class ImageView
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
                new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Fill},
            }
        };
    }
}