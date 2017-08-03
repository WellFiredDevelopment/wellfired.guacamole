using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;

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
        float InitialOffset { get; }
        int ScrollBarSize { get; }

        /// <summary>
        /// ScrollTo a specific item.
        /// </summary>
        /// <param name="item">The item you wish to scroll to. This should be the items bindableObject, not the visual element.</param>
        void ScrollTo(object item);
    }
}