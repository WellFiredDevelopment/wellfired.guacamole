using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Cells
{
    public partial class Cell : View, ICell
    {
        protected Cell()
        {
            MinSize = UISize.Of(50, 20);
        }

        public ListView Container { get; set; }
    }
}