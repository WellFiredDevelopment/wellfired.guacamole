using System.Collections.Generic;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Taskist.View.Cells
{
    public class FilterCell : Cell
    {
        public FilterCell()
        {
            var filterColor = new Button();
            var filterName = new Label {Text = "Filter"};

            Content = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                Children = new List<ILayoutable> {
                    filterColor,
                    filterName
                }
            };
        }
    }
}