using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    /// <summary>
    /// IListView is the interface used on Views of type List. Currently, this is only used for layouting code.
    /// </summary>
    public interface IListView : IHasChildren
    {
        int EntrySize { get; }
        int TotalContentSize { get; }
        int Spacing { get; }
        OrientationOptions Orientation { get; }
        float AvailableSpace { get; set; }
        INotifyPropertyChanged SelectedItem { set; }
        float InitialOffset { get; }
        int ScrollBarSize { get; }
        bool CanScroll { get; }
        float ScrollOffset { get; }

        /// <summary>
        /// ScrollTo a specific item.
        /// </summary>
        /// <param name="item">The item you wish to scroll to. This should be the items bindableObject, not the visual element.</param>
        void ScrollTo(object item);

        /// <summary>
        /// Returns the entry size for the passed BindableObject
        /// </summary>
        /// <param name="data">The object that is bound to a cell</param>
        /// <returns></returns>
        int GetEntrySizeFor(object data);
    }
}