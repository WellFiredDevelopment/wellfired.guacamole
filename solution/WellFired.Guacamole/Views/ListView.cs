using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class ListView : ItemsView, IListView
    {
        private readonly List<ICell> _activeEntries = new List<ICell>();
        public int TotalContentSize { get; private set; }

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
            BuildAdditionalEntries(NumberOfVisibleEntries - totalCurrentVisibleEntries);
        }

        private void BuildAdditionalEntries(int count)
        {
            var initialIndex = _activeEntries.Count;
            for (var n = 0; n < count; n++)
            {
                var data = ItemSource[initialIndex + n];
                
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
                Children.Add(cell as ILayoutable);
            }
        }
    }
}