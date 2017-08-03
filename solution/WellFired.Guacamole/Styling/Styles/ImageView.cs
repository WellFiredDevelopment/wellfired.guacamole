using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class ImageView
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGBA(125, 125, 125, 255)},
                new Setter {Property = View.OutlineColorProperty, Value = UIColor.FromRGBA(125, 125, 125, 255)},
                new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
                new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Fill},
            }
        };
    }
}