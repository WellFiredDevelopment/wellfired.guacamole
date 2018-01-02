using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class ListView
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(245, 245, 245)},
                new Setter {Property = View.OutlineColorProperty, Value = UIColor.FromRGB(245, 245, 245)},
                new Setter {Property = View.OutlineThicknessProperty, Value = 0},
                new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
                new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Fill},
                
                new Setter {Property = Views.ListView.ScrollBarBackgroundColorProperty, Value = UIColor.FromRGBA(77, 77, 77, 255)},
                new Setter {Property = Views.ListView.ScrollBarOutlineColorProperty, Value = UIColor.FromRGBA(54, 54, 54, 255)},
                new Setter {Property = Views.ListView.ScrollBarCornerMaskProperty, Value = CornerMask.All},
                new Setter {Property = Views.ListView.ScrollBarCornerRadiusProperty, Value = 6.0},
                new Setter {Property = Views.ListView.ScrollBarSizeProperty, Value = 10},
            }
        };
    }
}