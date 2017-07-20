using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Exceptions;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    /// <summary>
    /// The ListView is a View that supports dynamic content and scrollable views. It can have an Orientation of either
    /// Horizontal or Vertical. On top of that, the view can be set to have a dynamic data source, if the ItemSource is
    /// an ObservableCollection, when you add, remove, insert or in any way change that collection, the ListView
    /// will be set to update dynamically.
    /// </summary>
    public partial class ListView : ItemsView, IListView, IListensToVdsChanges
    {
        private List<int> _visualDataSet = new List<int>();
        private readonly List<ICell> _activeEntries = new List<ICell>();
        private readonly List<ICell> _inactiveEntries = new List<ICell>();
        public int TotalContentSize { get; private set; }
        public float InitialOffset { get; set; }

        public ListView()
        {
            VerticalLayout = LayoutOptions.Fill;
            HorizontalLayout = LayoutOptions.Fill;
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
            ReCalculateTotalContentSize();
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

            InvalidateRectRequest();
            var viewSize = SizingHelper.GetImportantSize(Orientation, RectRequest);
            CanScroll = viewSize < TotalContentSize;
            CalculateVisualDataSet();
        }

        private void CalculateVisualDataSet()
        {
            var newVds = ListViewCalculator.CalculateVisualDataSet(-ScrollOffset, NumberOfVisibleEntries * EntrySize, EntrySize, TotalContentSize, Spacing);
            var oldVds = _visualDataSet;
            ListViewCalculator.AdjustForNewVds(oldVds, newVds, this);
            _visualDataSet = newVds.ToList();
            InitialOffset = ListViewCalculator.CalculateInitialOffset(_visualDataSet, EntrySize, Spacing) + ScrollOffset;
        }

        public void ItemLeftVds(int vdsIndex)
        {
            var data = ItemSource[vdsIndex];
            foreach (var child in Children)
            {
                var cell = (ICell) child;
                if (!cell.BindingContext.Equals(data)) 
                    continue;
                
                _inactiveEntries.Add(cell);
                _activeEntries.Remove(cell);
                Children.Remove(child);
                return;
            }                
        }

        public void ItemEnteredVds(int vdsIndex, bool front)
        {
            var data = ItemSource[vdsIndex];
            ICell cell;
            if (_inactiveEntries.Any())
            {
                cell = _inactiveEntries.First();
                CellHelper.ReUseCell(cell, data);
                _inactiveEntries.Remove(cell);
                _activeEntries.Add(cell);
            }
            else
            {
                cell = GetNewCell(data);
                _activeEntries.Add(cell);
            }
            
            if (front)
                Children.Insert(0, cell as ILayoutable);
            else
                Children.Add(cell as ILayoutable);
        }

        private ICell GetNewCell(object data)
        {
            var cell = ItemTemplate == null
                ? CellHelper.CreateDefaultCell(data, this)
                : CellHelper.CreateCellWith(ItemTemplate, data, this);

            if (cell == null)
                throw new NoCompatibleCellInDataTemplate();

            cell.SetStyleDictionary(StyleDictionary);

            var rectRequest = cell.RectRequest;

            switch (Orientation)
            {
                case OrientationOptions.Horizontal:
                    rectRequest.Width = EntrySize;
                    break;
                case OrientationOptions.Vertical:
                    rectRequest.Height = EntrySize;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _activeEntries.Add(cell);
            return cell;
        }
    }
}