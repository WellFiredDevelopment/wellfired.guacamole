using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public interface IListView : IHasChildren
    {
        int EntrySize { get; }
        int TotalContentSize { get; set; }
        int Spacing { get; set; }
        OrientationOptions Orientation { get; set; }
    }
}