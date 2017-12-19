using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Layouts
{
    public static class AdjacentLayoutCellCalculator
    {   
        public static void Calculate(ICollection<ILayoutable> cellArray, UIRect availableSpace, OrientationOptions orientation, int spacing)
        {
            var totalLostBySpacing = spacing * (cellArray.Count - 1);
            
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
                var layout = getImportantLayout(o);
                return layout == LayoutOptions.Expand || anyFill && layout == LayoutOptions.Center;
            }).ToArray();
            
            // If we have fill elements, then only fill elements are considered to be dynamic, if we have center
            // elements and no fill elements, only centered elements are considered to be dynamic.
            var dynamicElements = cellArray.Where(o => {
                if (onlyExpand)
                    return false;
                
                var layout = getImportantLayout(o);
                if (anyFill && layout == LayoutOptions.Fill)
                    return true;

                return !anyFill && layout == LayoutOptions.Center;
            }).ToArray();

            var staticSpace = staticElements.Select(o => o.RectRequest).Sum(getImportantSize);
            var dynamicSpace = !dynamicElements.Any() ? 0.0f : getImportantSize(availableSpace) - totalLostBySpacing - staticSpace;
            var dynamicSharedSpace = !dynamicElements.Any() ? 0 : dynamicSpace / dynamicElements.Length;

            // If the statically allocated space is greater than the space available to static entries
            // we must shrink these static entries to fit inside the available space.
            var maxAvailableToStaticEntries = getImportantSize(availableSpace) - dynamicSpace;
            if (staticSpace > maxAvailableToStaticEntries)
            {
                var total = 0;
                var processedCounter = 0;
                var ordered = staticElements.OrderBy(o => getImportantSize(o.RectRequest));
                foreach (var entry in ordered)
                {
                    total += (int)getImportantSize(entry.RectRequest);
                    if (total <= maxAvailableToStaticEntries)
                    {
                        processedCounter++;
                        continue;
                    }
                 
                    total -= (int)getImportantSize(entry.RectRequest);
                    break;
                }

                if (processedCounter < staticElements.Length)
                {
                    var rest = ordered.Skip(processedCounter).ToArray();
                    var staticSharedSpace = (maxAvailableToStaticEntries - total) / rest.Length;

                    foreach (var entry in rest)
                    {
                        var rect = entry.RectRequest;
                        
                        if (orientation == OrientationOptions.Horizontal)
                            rect.Width = (int) staticSharedSpace;
                        if (orientation == OrientationOptions.Vertical)
                            rect.Height = (int) staticSharedSpace;

                        entry.RectRequest = rect;
                        entry.ContentRectRequest = rect;
                    }
                }
            }

            foreach (var element in dynamicElements)
            {
                var rect = element.RectRequest;
                
                if (orientation == OrientationOptions.Horizontal)
                    rect.Width = (int)dynamicSharedSpace;
                else
                    rect.Height = (int)dynamicSharedSpace;
                
                if (orientation == OrientationOptions.Horizontal)
                    rect.Height = (int)getUnImportantSize(availableSpace);
                if (orientation == OrientationOptions.Vertical)
                    rect.Width = (int)getUnImportantSize(availableSpace);
                
                element.RectRequest = rect;
            }
            
            foreach (var element in staticElements)
            {
                element.RectRequest = UIRect.From(element.RectRequest.Size);
                var layout = getUnImportantLayout(element);
                if (layout == LayoutOptions.Expand)
                    continue;
                
                var rect = element.RectRequest;
                if (orientation == OrientationOptions.Horizontal)
                    rect.Height = (int)getUnImportantSize(availableSpace);
                if (orientation == OrientationOptions.Vertical)
                    rect.Width = (int)getUnImportantSize(availableSpace);

                element.RectRequest = rect;
            }
        }

        private static void HasFillAndCenter(ICollection<ILayoutable> cellArray, OrientationOptions orientation, out bool anyFill, out bool anyCenter)
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
                var layout = getLayout(cell);
                if (layout == LayoutOptions.Center)
                    anyCenter = true;
                if (layout == LayoutOptions.Fill)
                    anyFill = true;
            }
        }
    }
}