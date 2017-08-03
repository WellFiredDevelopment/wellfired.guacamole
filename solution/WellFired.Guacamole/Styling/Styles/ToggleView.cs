using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class ToggleView
    {
        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Expand},
                new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
            }
        };
    }
}