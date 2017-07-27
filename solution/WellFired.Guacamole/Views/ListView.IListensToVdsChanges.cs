using System.Linq;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class ListView : IListensToVdsChanges
    {
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

            var layoutable = (ILayoutable)cell;

            layoutable.X = int.MaxValue;
            
            if (front)
                Children.Insert(0, layoutable);
            else
                Children.Add(layoutable);
        }
    }
}