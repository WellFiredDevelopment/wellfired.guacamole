using System;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class ListView : IListensToVdsChanges
    {
        public void ItemLeftVds(int vdsIndex, bool front)
        {
            var data = GetItem(vdsIndex);
            
            if(front)
                InitialOffset = 0;
            
            foreach (var child in Children)
            {
                var cell = (ICell) child;
                if (!cell.BindingContext.Equals(data)) 
                    continue;
                
                Cache(cell);
                _activeCells.Remove(cell);
                Children.Remove(child);
                return;
            }                
        }

        public void ItemEnteredVds(int vdsIndex, bool front)
        {
            var data = GetItem(vdsIndex);
            
            if(front)
                InitialOffset = -GetEntrySizeFor(data);
            
            if(data == null)
                throw new Exception("Failed to find VDS data for given index.");
            
            var cell = Retrieve(data);
            if (cell != default(ICell))
            {
                CellHelper.ReUseCell(cell, data);
                _activeCells.Add(cell);
            }
            else
            {
                cell = GetAReusableCell(data);
                _activeCells.Add(cell);
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