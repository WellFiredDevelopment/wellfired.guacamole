using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public partial class View : ILayoutable
    {
        public int X
        {
            get { return _validRectRequest.X; }
            set { _validRectRequest.X = value; }
        }

        public int Y
        {
            get { return _validRectRequest.Y; }
            set { _validRectRequest.Y = value; }
        }
    }
}