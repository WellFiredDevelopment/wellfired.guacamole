using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Layouts
{
    public interface IVirtualCell
    {
        UIRect PositionInCell { get; }
        UIRect Rect { get; }
        ILayoutable Layoutable { get; }
    }
}