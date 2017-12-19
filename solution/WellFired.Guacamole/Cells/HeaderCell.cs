using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Cells
{
    public partial class HeaderCell : Cell
    {
        public HeaderCell()
        {
            Style = Styling.Styles.HeaderCell.Style;
            
            Bind(TextProperty, "CellLabelText", BindingMode.ReadOnly);
        }
    }
}