using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Layouts
{
    public static class AdjacentLayoutCellCalculator
    {
        public static IEnumerable<IVirtualCell> GetCellsFromLayoutablesArray(
            ICollection<ILayoutable> layoutablesArray, 
            UIRect availableSpace, 
            OrientationOptions orientation, 
            int spacing)
        {
            var cellArray = new VirtualCell[layoutablesArray.Count];
            for (var index = 0; index < layoutablesArray.Count; index++)
                cellArray[index] = new VirtualCell {Layoutable = layoutablesArray.ElementAt(index)};
            
            BuildCells(cellArray, availableSpace, orientation, spacing);

            foreach (var cell in cellArray)
                cell.CalculatePositionInCell();
            
            return cellArray;
        }
        
        private static void BuildCells(ICollection<VirtualCell> cellArray, UIRect availableSpace, OrientationOptions orientation, int spacing)
        {
            var getImportantLayout = new Func<ILayoutable, LayoutOptions>(layoutable =>
                orientation == OrientationOptions.Horizontal
                    ? layoutable.HorizontalLayout
                    : layoutable.VerticalLayout);
            
            var getUnImportantLayout = new Func<ILayoutable, LayoutOptions>(layoutable =>
                orientation == OrientationOptions.Horizontal
                    ? layoutable.VerticalLayout
                    : layoutable.HorizontalLayout);
            
            var getImportantSize = new Func<UIRect, float>(rect =>
                orientation == OrientationOptions.Horizontal
                    ? rect.Width
                    : rect.Height);
            
            var getUnImportantSize = new Func<UIRect, float>(rect =>
                orientation == OrientationOptions.Horizontal
                    ? rect.Height
                    : rect.Width);
            
            bool anyFill;
            bool anyCenter;
            HasFillAndCenter(cellArray, orientation, out anyFill, out anyCenter);
            var onlyExpand = !anyFill && !anyCenter;

            // A Static element is an element with expand. If we have any elements who are set to fill, then centered 
            // elements are also considered static.
            var staticElements = cellArray.Where(o => {
                var layout = getImportantLayout(o.Layoutable);
                return layout == LayoutOptions.Expand || anyFill && layout == LayoutOptions.Center;
            }).ToArray();
            
            // If we have fill elements, then only fill elements are considered to be dynamic, if we have center
            // elements and no fill elements, only centered elements are considered to be dynamic.
            var dynamicElements = cellArray.Where(o => {
                if (onlyExpand)
                    return false;
                
                var layout = getImportantLayout(o.Layoutable);
                if (anyFill && layout == LayoutOptions.Fill)
                    return true;

                return layout == LayoutOptions.Center;
            }).ToArray();

            var staticWidth = staticElements.Select(o => o.Layoutable.RectRequest).Sum(getImportantSize);
            var dynamicWidth = getImportantSize(availableSpace) - staticWidth;
            var dynamicSharedSpace = !dynamicElements.Any() ? 0 : dynamicWidth / dynamicElements.Length;

            foreach (var element in dynamicElements)
            {
                var rect = element.Rect;
                
                if (orientation == OrientationOptions.Horizontal)
                    rect.Width = (int)dynamicSharedSpace;
                else
                    rect.Height = (int)dynamicSharedSpace;
                
                if (orientation == OrientationOptions.Horizontal)
                    rect.Height = (int)getUnImportantSize(availableSpace);
                if (orientation == OrientationOptions.Vertical)
                    rect.Width = (int)getUnImportantSize(availableSpace);
                
                element.Rect = rect;
            }
            
            foreach (var element in staticElements)
            {
                element.Rect = UIRect.From(element.Layoutable.RectRequest.Size);
                var layout = getUnImportantLayout(element.Layoutable);
                if (layout == LayoutOptions.Expand)
                    continue;
                
                var rect = element.Rect;
                if (orientation == OrientationOptions.Horizontal)
                    rect.Height = (int)getUnImportantSize(availableSpace);
                if (orientation == OrientationOptions.Vertical)
                    rect.Width = (int)getUnImportantSize(availableSpace);
                element.Rect = rect;
            }
        }

        private static void HasFillAndCenter(ICollection<VirtualCell> cellArray, OrientationOptions orientation, out bool anyFill, out bool anyCenter)
        {
            var getLayout = new Func<ILayoutable, LayoutOptions>(layoutable =>
                orientation == OrientationOptions.Horizontal
                    ? layoutable.HorizontalLayout
                    : layoutable.VerticalLayout);

            anyCenter = false;
            anyFill = false;

            for (var index = 0; index < cellArray.Count; index++)
            {
                var cell = cellArray.ElementAt(index);
                var layout = getLayout(cell.Layoutable);
                if (layout == LayoutOptions.Center)
                    anyCenter = true;
                if (layout == LayoutOptions.Fill)
                    anyFill = true;
            }
        }
    }
}