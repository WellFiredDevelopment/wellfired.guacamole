using System.Collections.Generic;
using WellFired.Guacamole.Examples.DotPeek.Layout;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.DotPeek.UIElementFactory
{
    public class Grid
    {
        private readonly List<ILayoutable> _rows = new List<ILayoutable>();

        public void AddRow(params ILayoutable[] cells)
        {
            var row = LayoutFactory.CreateHorizontalLayout(cells);
            _rows.Add(row);
        }

        public LayoutView GetGrid()
        {
            return LayoutFactory.CreateVerticalLayout(_rows.ToArray());
        }

        public static ILayoutable GetEmptyView()
        {
            return new View
            {
                BackgroundColor = UIColor.FromRGB(40, 40, 40),
                OutlineColor = UIColor.FromRGB(40, 40, 40),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill
            };
        }
    }
}