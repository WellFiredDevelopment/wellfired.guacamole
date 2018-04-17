using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    /// <summary>
    /// The ListView is a View that supports dynamic content and scrollable views. It can have an Orientation of either
    /// Horizontal or Vertical. On top of that, the view can be set to have a dynamic data source, if the ItemSource is
    /// an ObservableCollection, when you add, remove, insert or in any way change that collection, the ListView
    /// will be set to update dynamically.
    /// The ListView contains a series of visible cells. These visible cells are recycled for performance reasons. To 
    /// calculate what should be visible we use the VdsCalculator, that operates on a Visual Data Set. If our view
    /// is big enough to view 4 entries at once, our VDS will be the four indicies into this data those visible elements
    /// represent. Entries leaving or entering the VDS are what trigger new cells to be created. 
    /// </summary>
    public partial class ListView : ItemsView, IListView
    {
        // This is our currently displayed VDS
        private List<int> _visualDataSet = new List<int>();
        
        // We keep a list of new VDS here to avoid allocations, it's cleared at the beginning of every calculation
        private List<int> _newVds = new List<int>();
        
        private readonly List<ICell> _activeCells = new List<ICell>();
        private readonly Dictionary<Type, List<ICell>> _inactiveCells = new Dictionary<Type, List<ICell>>();
        
        /// <summary>
        /// The total width for horizontal list view, or the total height for vertical list view, after suming up the size of each
        /// items.
        /// </summary>
        public int TotalContentSize { get; private set; }

        /// <summary>
        /// The position where the first child should be rendered. A negative value indicate that the first child is rendered above the
        /// list view position (or on the left for a horizontal list view), meaning part of it is outside of the list view. This happens
        /// when scrolling, or when adding and removing children from the list of cells to render.
        /// </summary>
        public float InitialOffset { get; private set; }

        private int _headersCount;
        private int _itemsCount;
        public Action<INotifyPropertyChanged, SelectedItemChangedEventArgs> OnItemSelected { get; set; } = delegate {  };

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
            var spacing = (_headersCount + _itemsCount - 1) * Spacing;
            TotalContentSize = spacing + _headersCount * HeaderSize + _itemsCount * EntrySize;
            InvalidateRectRequest();
        }

        protected override void ItemSourceChanged()
        {
            FigureOutGroup();
            
            Children.Clear();

            foreach (var cell in _activeCells)
                Cache(cell);
            
            _activeCells.Clear();
            _visualDataSet = new List<int>();
            ReCalculateTotalContentSize();

            if (TotalContentSize <= 0) 
                return;
            
            var viewSize = SizingHelper.GetImportantSize(Orientation, RectRequest);
            CanScroll = viewSize < TotalContentSize;
            CalculateVisualDataSet();
        }

        private void FigureOutGroup()
        {
            _headersCount = 0;
            _itemsCount = 0;

            if (ItemSourceCount == 0)
                return;
            
            if (IsItemSourceContiguous)
            {
                _itemsCount = ItemSourceCount;
                return;
            }

            // we intentionally use the raw itemsource here, since that will give use the header information if the collection
            // is not contiguous
            _headersCount = ItemSource.Count;
            _itemsCount = ItemSourceCount - _headersCount;
        }

        protected override void ItemSourceCleared()
        {
            ItemSourceChanged();
        }

        protected override void ItemAdded(object item, int index)
        {
            var headerAdded = 0;
            var itemAdded = 0;
            
            if (item is ICollection)
            {
                _headersCount++;
                headerAdded = 1;
            }
            else
            {
                _itemsCount++;
                itemAdded = 1;
            }

            ReCalculateTotalContentSize();
            var viewSize = SizingHelper.GetImportantSize(Orientation, RectRequest);
            CanScroll = viewSize < TotalContentSize;

            if (CanScroll)
            {
                if (_visualDataSet.Count > 0 && _visualDataSet[0] >= index)
                {
                    var spacing = (headerAdded + itemAdded) * Spacing;
                    ScrollOffset += headerAdded * HeaderSize + itemAdded * EntrySize + spacing;
                }
            }
            
            CalculateVisualDataSet();
        }

        protected override void ItemRemoved(object item)
        {
            var removedCell = _activeCells.FirstOrDefault(o => Equals(o.BindingContext, item));
            
            // If we find a default cell it means we're not rendering this element, so we don't need to worry about this
            if (removedCell != default(ICell))
            {
                Cache(removedCell);
                _activeCells.Remove(removedCell);
                Children.Remove((ILayoutable)removedCell);    
            }
            
            if (item is ICollection)
            {
                _headersCount--;
            }
            else
            {
                _itemsCount--;
            }
            
            ReCalculateTotalContentSize();
            var viewSize = SizingHelper.GetImportantSize(Orientation, RectRequest);
            CanScroll = viewSize < TotalContentSize;
            
            // We do this here to reclamp the scroll value in case item being removed place us outside of the scrolling limits.
            ScrollOffset = CanScroll ? ScrollOffset : 0;
            
            CalculateVisualDataSet();
        }

        protected override void ItemReplaced(object oldItem, object newItem, int index)
        {
            // if we don't have this item in the VDS, we don't need to attempt to replace the binding context on it, since it's not rendering.
            if (!_visualDataSet.Contains(index))
                return;
            
            var replacedCell = _activeCells.FirstOrDefault(o => Equals(o.BindingContext, oldItem));
            CellHelper.ReUseCell(replacedCell, newItem);
        }

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == SelectedItemProperty.PropertyName)
                SetSelectedItem();

            if (e.PropertyName != AvailableSpaceProperty.PropertyName) 
                return;
            
            var totalCurrentVisibleEntries = _activeCells.Count;
            if (totalCurrentVisibleEntries > AvailableSpace)
                return;

            if (TotalContentSize <= 0)
                return;

            InvalidateRectRequest();
            var viewSize = SizingHelper.GetImportantSize(Orientation, RectRequest);
            CanScroll = viewSize < TotalContentSize;
            
            // We do this here to reclamp the scroll value incase we've pulled the bottom of a listView too far.
            ScrollOffset = CanScroll ? ScrollOffset : 0;

            CalculateVisualDataSet();
        }
        
        /// <summary>
        /// This methods works internally and mathmatically to work out which indicies this list should be rendering. We will
        /// generate a list of indiciest o display and compare this to our previous update, if anything needs adding or removing
        /// it will be handled by AdjustForNewVds.
        /// </summary>
        private void CalculateVisualDataSet()
        {
            _newVds.Clear();
            
            var scrollOffset = ScrollOffset;
            VdsCalculator.CalculateVisualDataSetWithVariableHeight(scrollOffset, AvailableSpace, ItemSourceCount, GetEntrySizeFor, ref _newVds);
            VdsCalculator.AdjustForNewVds(_visualDataSet, _newVds, this);
            
            // Here we clone this so we can compare on the next CalculateVisualDataSet
            _visualDataSet = _newVds.ToList();
        }

        /// <summary>
        /// This will return a cell from either our internal cache or build a new one for you.
        /// When cells are returned, they should already have their binding context set, so the caller does not have to worry about this.
        /// </summary>
        /// <param name="data">The object for which we'd like to find a cell and bind</param>
        /// <returns></returns>
        /// <exception cref="NoCompatibleCellInDataTemplate"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private ICell GetAReusableCell(object data)
        {
            var itemTemplate = GetTemplateFor(data);
            var entrySize = GetEntrySizeFor(data);
            
            var cell = itemTemplate == null
                ? CellHelper.CreateDefaultCell(data, this, StyleDictionary)
                : CellHelper.CreateCellWith(this, itemTemplate, data, this, StyleDictionary);

            if (cell == null)
                throw new NoCompatibleCellInDataTemplate();

            var rectRequest = cell.RectRequest;
            var contentRectRequest = cell.ContentRectRequest;

            switch (Orientation)
            {
                case OrientationOptions.Horizontal:
                    rectRequest.Width = entrySize + Spacing;
                    contentRectRequest.Width = entrySize;
                    break;
                case OrientationOptions.Vertical:
                    rectRequest.Height = entrySize + Spacing;
                    contentRectRequest.Height = entrySize;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            cell.RectRequest = rectRequest;
            cell.ContentRectRequest = contentRectRequest;
            return cell;
        }

        /// <inheritdoc />
        /// <summary>
        /// ScrollTo a specific item.
        /// </summary>
        /// <param name="item">The item you wish to scroll to. This should be the items bindableObject, not the visual element.</param>
        public void ScrollTo(object item)
        {
            var index = GetIndexOf(item);
            if(index == -1)
                throw new IndexOutOfRangeException();

            ScrollOffset = VdsCalculator.DesiredScrollFor(index, ItemSourceCount, GetEntrySizeFor);
        }

        /// <summary>
        /// This method will put a cell back into our internal cache. Cells can be reused later by calling the retrieve method
        /// </summary>
        /// <param name="cell">The cell to cache</param>
        private void Cache(ICell cell)
        {
            var bindingContext = cell.BindingContext.GetType();
            if (!_inactiveCells.TryGetValue(bindingContext, out var list))
            {
                list = new List<ICell>();
                _inactiveCells[bindingContext] = list;
            }
            
            list.Add(cell);
        }

        /// <summary>
        /// We will attempt to retrieve an item from our internal cell cache.
        /// </summary>
        /// <param name="forBoundObject">The bound object for which we want to find a view</param>
        /// <returns>either a valid reusable item or default(ICell).</returns>
        private ICell Retrieve(object forBoundObject)
        {
            var bindingObjectType = forBoundObject.GetType();
            if (!_inactiveCells.TryGetValue(bindingObjectType, out var _))
                return default(ICell);

            if(!_inactiveCells[bindingObjectType].Any())
                return default(ICell);
            
            var cell = _inactiveCells[bindingObjectType].First();
            _inactiveCells[bindingObjectType].Remove(cell);
            return cell;
        }

        /// <summary>
        /// This method will get either the header or entry data templace depending on the object that was passed.
        /// </summary>
        /// <param name="data">The Bound object whos size we want to check.</param>
        /// <returns></returns>
        private DataTemplate GetTemplateFor(object data)
        {
            if (data is ICollection)
                return HeaderTemplate;
            
            return ItemTemplate;
        }

        /// <summary>
        /// This method will return the EntrySize for a given element in the ItemSource if grouping is not enabled, we will always
        /// immediately return the default entry size, if grouping is enabled, we shall return either the HeaderSize or the EntrySize 
        /// depending on which element is passed.
        /// </summary>
        /// <param name="index">The index for which to get the entry</param>
        /// <returns></returns>
        private int GetEntrySizeFor(int index)
        {
            if (IsItemSourceContiguous)
                return EntrySize + Spacing;

            var entry = GetItem(index);

            return entry is ICollection ? HeaderSize + Spacing : EntrySize + Spacing;
        }

        /// <summary>
        /// This method will return the EntrySize for a given element in the ItemSource if grouping is not enabled, we will always
        /// immediately return the default entry size, if grouping is enabled, we shall return either the HeaderSize or the EntrySize 
        /// depending on which element is passed.
        /// </summary>
        /// <param name="data">The Bound object whos size we want to check.</param>
        /// <returns></returns>
        public int GetEntrySizeFor(object data)
        {
            if (IsItemSourceContiguous)
                return EntrySize;

            if (data is ICollection)
                return HeaderSize;
            
            return EntrySize;
        }
    }
}