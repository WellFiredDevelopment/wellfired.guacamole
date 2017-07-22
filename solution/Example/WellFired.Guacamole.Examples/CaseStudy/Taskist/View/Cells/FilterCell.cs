using WellFired.Guacamole.Cells;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.View.Cells
{
    public partial class FilterCell : Cell
    {
        public FilterCell()
        {
            Bind(FilterTextProperty, "FilterName");
            Bind(FilterColorProperty, "FilterColor");
            Bind(IsSelectedProperty, "IsSelected");
        }
    }
}