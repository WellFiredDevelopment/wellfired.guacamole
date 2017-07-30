using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    /// <summary>
    /// The ListView is a View that supports dynamic content and scrollable views. It can have an Orientation of either
    /// Horizontal or Vertical. On top of that, the view can be set to have a dynamic data source, if the ItemSource is
    /// an ObservableCollection, when you add, remove, insert or in any way change that collection, the ListView
    /// will be set to update dynamically.
    /// 
    /// The ListView contains a series of visible cells. These visible cells are recycled for performance reasons. To 
    /// calculate what should be visible we use the VdsCalculator, that operates on a Visual Data Set. If our view
    /// is big enough to view 4 entries at once, our VDS will be the four indicies into this data those visible elements
    /// represent. Entries leaving or entering the VDS are what trigger new cells to be created. 
    /// </summary>
    public partial class ListView : ItemsView, IListView
    {
        private List<int> _visualDataSet = new List<int>();
        private readonly List<ICell> _activeEntries = new List<ICell>();
        private readonly List<ICell> _inactiveEntries = new List<ICell>();
        private bool _hasBeenLayouted;
        private object _cachedScrollTo;
        public int TotalContentSize { get; private set; }
        public float InitialOffset { get; private set; }

        public ListView()
        {
            Style = Styling.Styles.ListView.Style;
        }

        /// <summary>
        /// When the ItemSource Collection is modified, this method should be called, the Total Content Size is used 
        /// for all scroll / sizing calculations
        /// </summary>
        private void ReCalculateTotalContentSize()
        {
            var count = ItemSource.Count;
            TotalContentSize = (count - 1) * Spacing + count * EntrySize;
            InvalidateRectRequest();
        }

        protected override void ItemSourceChanged()
        {
            Children.Clear();
            
            foreach (var entry in _activeEntries)
                _inactiveEntries.Add(entry);
            
            _activeEntries.Clear();
            _visualDataSet = new List<int>();
            ReCalculateTotalContentSize();
            
            if (TotalContentSize > 0)
                CalculateVisualDataSet();
        }

        protected override void ItemSourceCleared()
        {
            ItemSourceChanged();
        }

        protected override void ItemAdded(object item, int index)
        {
            ReCalculateTotalContentSize();
        }

        protected override void ItemRemoved(object item)
        {
            ReCalculateTotalContentSize();
        }

        protected override void ItemReplaced(object oldItem, object newItem, int index)
        {
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName != NumberOfVisibleEntriesProperty.PropertyName)
                return;

            var totalCurrentVisibleEntries = _activeEntries.Count;
            if (totalCurrentVisibleEntries > NumberOfVisibleEntries)
                return;

            if (TotalContentSize <= 0)
                return;
            
            InvalidateRectRequest();
            var viewSize = SizingHelper.GetImportantSize(Orientation, RectRequest);
            CanScroll = viewSize < TotalContentSize;
            ScrollOffset = ScrollOffset; // We do this here to reclamp the scroll value incase we've pulled the bottom of a listView too far.
            CalculateVisualDataSet();

            if (_hasBeenLayouted || _cachedScrollTo == null) 
                return;
            
            _hasBeenLayouted = true;
            ScrollTo(_cachedScrollTo);
            _cachedScrollTo = null;
        }

        private void CalculateVisualDataSet()
        {
            var newVds = VdsCalculator.CalculateVisualDataSet(-ScrollOffset, NumberOfVisibleEntries * EntrySize, EntrySize, TotalContentSize, Spacing).ToArray();
            var oldVds = _visualDataSet;
            VdsCalculator.AdjustForNewVds(oldVds.ToArray(), newVds, this);
            _visualDataSet = newVds.ToList();
            InitialOffset = VdsCalculator.CalculateInitialOffset(_visualDataSet, EntrySize, Spacing) + ScrollOffset;
        }

        private ICell GetNewCell(object data)
        {
            var cell = ItemTemplate == null
                ? CellHelper.CreateDefaultCell(data, this)
                : CellHelper.CreateCellWith(this, ItemTemplate, data, this);

            if (cell == null)
                throw new NoCompatibleCellInDataTemplate();

            cell.SetStyleDictionary(StyleDictionary);

            var rectRequest = cell.RectRequest;
            var contentRectRequest = cell.ContentRectRequest;

            switch (Orientation)
            {
                case OrientationOptions.Horizontal:
                    rectRequest.Width = EntrySize + Spacing;
                    contentRectRequest.Width = EntrySize;
                    break;
                case OrientationOptions.Vertical:
                    rectRequest.Height = EntrySize + Spacing;
                    contentRectRequest.Height = EntrySize;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            cell.RectRequest = rectRequest;
            cell.ContentRectRequest = contentRectRequest;
            return cell;
        }
        
        /// <summary>
        /// ScrollTo a specific item.
        /// </summary>
        /// <param name="item">The item you wish to scroll to. This should be the items bindableObject, not the visual element.</param>
        public void ScrollTo(object item)
        {
            if (!_hasBeenLayouted)
            {
                _cachedScrollTo = item;
                return;
            }
            
            var index = ItemSource.IndexOf(item);
            if(index == -1)
                throw new IndexOutOfRangeException();
            
            ScrollOffset = VdsCalculator.DesiredScrollFor(index, EntrySize, Spacing);
        }
    }
}