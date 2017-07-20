using System.ComponentModel;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public interface IListView : IHasChildren
    {
        int EntrySize { get; }
        int TotalContentSize { get; }
        int Spacing { get; }
        OrientationOptions Orientation { get; }
        int NumberOfVisibleEntries { get; set; }
        INotifyPropertyChanged SelectedItem { set; }
        float ScrollOffset { get; }
        float InitialOffset { get; }
    }
}