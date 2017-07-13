using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Layouts
{
    public interface IVirtualCell
    {
        UIRect PositionInCell { get; }
        UIRect Rect { get; set; }
        ILayoutable Layoutable { get; }
        void CalculatePositionInCell();
    }
}