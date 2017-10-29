using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class View : ILayoutable
    {
        public float X
        {
            get => _validRectRequest.X;
            set => _validRectRequest.X = value;
        }

        public float Y
        {
            get => _validRectRequest.Y;
            set => _validRectRequest.Y = value;
        }
    }
}