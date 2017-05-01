using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Cells
{
    public class Cell : Views.View, ICell
    {
        protected Cell()
        {
            MinSize = UISize.Of(50, 20);
        }
    }
}