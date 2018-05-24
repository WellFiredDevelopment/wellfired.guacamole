using System;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class ListView : IListensToVdsChanges
    {
        /// <summary>
        /// When an item becomes invisible, we cache the cell and remove it from the children.
        /// </summary>
        /// <param name="vdsIndex"></param>
        public void ItemLeftVds(int vdsIndex)
        {
            var data = GetItem(vdsIndex);

            //the data may not exist anymore because was removed from the list.
            if (data == null)
                return;
            
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

        /// <summary>
        /// When an item becomes visible, we get a cell from the cache and we inject the data in it.
        /// </summary>
        /// <param name="vdsIndex"></param>
        /// <param name="front">indicate if the item added is on the top of already visible children, or if it is at the bottom
        /// (left or right for horizontal list view)</param>
        /// <exception cref="Exception"></exception>
        public void ItemEnteredVds(int vdsIndex, bool front)
        {
            var data = GetItem(vdsIndex);
            
            if(data == null)
                throw new Exception("Failed to find VDS data for given index " + vdsIndex);
            
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