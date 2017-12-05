using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
	internal static class KeyValueCell
	{
        private static readonly UIColor DarkerColor = UIColor.FromRGB(245, 245, 245);

        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.BackgroundColorProperty, Value = DarkerColor},
                new Setter {Property = View.OutlineColorProperty, Value = DarkerColor},
                new Setter {Property = View.OutlineThicknessProperty, Value = 0.0},
            }
        };
	}
}