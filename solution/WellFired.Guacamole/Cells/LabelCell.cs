using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Cells
{
    public partial class LabelCell : Cell
    {
        public LabelCell()
        {
            MinSize = UISize.Of(50, 20);
            Style = Styling.Styles.LabelCell.Style;
        }
    }
}