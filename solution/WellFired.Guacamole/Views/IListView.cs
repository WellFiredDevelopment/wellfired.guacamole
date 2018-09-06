using System;
using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    /// <summary>
    /// IListView is the interface used on Views of type List. Currently, this is only used for layouting code.
    /// </summary>
    public interface IListView : IHasChildren, INotifyPropertyChanged
    {
        Action<INotifyPropertyChanged, SelectedItemChangedEventArgs> OnItemSelected { get; }
        int TotalContentSize { get; }
        int Spacing { get; }
        OrientationOptions Orientation { get; }
        float AvailableSpace { get; set; }
        
        /// <summary>
        /// Setting this value select an item in the list view. It also unselects every other selected items, even
        /// if <see cref="SelectedItem"/> is set to null.
        /// </summary>
        INotifyPropertyChanged SelectedItem { get; set; }
        
        /// <summary>
        /// Adding items to this collection will select these items. When <see cref="SelectedItem"/> is set, every elements
        /// of the collection are unselected and the collection resetted event is sent.
        /// </summary>
        ObservableCollection<INotifyPropertyChanged> SelectedItems { get; set; }
        
        float InitialOffset { get; }
        int ScrollBarSize { get; }
        bool ShouldShowScrollBar { get; set; }
        bool CanScroll { get; }
        float ScrollOffset { get; }
        
        /// <summary>
        /// If true then several items can be selected by pressing Ctrl or Command.
        /// </summary>
        bool CanMultiSelect { get; }

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