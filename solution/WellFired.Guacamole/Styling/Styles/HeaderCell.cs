using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
    internal static class HeaderCell
    {
        private static readonly UIColor DarkerColor = UIColor.FromRGB(245, 245, 245);

        public static readonly Style Style = new Style
        {
            Setters =
            {
                new Setter {Property = View.BackgroundColorProperty, Value = DarkerColor},
                new Setter {Property = View.OutlineColorProperty, Value = DarkerColor},
                new Setter {Property = View.OutlineThicknessProperty, Value = 0.0},
                new Setter {Property = Cells.LabelCell.TextColorProperty, Value = UIColor.Black},
                new Setter {Property = Cells.Cell.CanMouseOverProperty, Value = false}
            }
        };
    }
}