using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells
{
    public partial class FilterCell : Cell
    {
        public FilterCell()
        {
            Bind(FilterTextProperty, "FilterName");
            Bind(FilterColorProperty, "FilterColor");
        }
    }
}