using System;
using System.Collections.Generic;
using System.Linq;

namespace WellFired.Guacamole.Views
{
    public static class ListViewCalculator
    {        
        /// <summary>
        /// Given the data that defines a visible layouted control, we can calculate a potentially visible data set.
        /// The Potentially visible data set would be a series of indicies into and arbritrary set of data.
        /// We calculate this data set using the paramteres that define the view of our control.
        /// </summary>
        public static IEnumerable<int> CalculateVisualDataSet(float virtualScrollPosition, int visibleControlSize, int estimatedElementSize, int estimatedContentSize, int spacing)
        {
            var visibleDataSet = new List<int>();
     
            var deltaX = estimatedElementSize + spacing;
            var sizeX = visibleControlSize;
     
            var minIndex = (int)Math.Floor(virtualScrollPosition / deltaX);
            var maxIndex = (int)Math.Floor((virtualScrollPosition + sizeX) / (deltaX + 0.1f)); // Here we add a small delta so we don't overun our buffer and select n + 1 visible elements
     
            var contentSizeX = estimatedContentSize;
            var max = (int)Math.Floor(contentSizeX * 2.0f / (deltaX + 0.1f));
     
            if (minIndex < 0)
                minIndex = 0;
            if (maxIndex > max)
                maxIndex = max;
     
            for (var iterationIndex = minIndex; iterationIndex <= maxIndex; iterationIndex++)
                visibleDataSet.Add(iterationIndex);
     
            return visibleDataSet;
        }

        public static void AdjustForNewVds(int [] oldVds, int [] newVds, IListensToVdsChanges listensToVdsChanges)
        {
            var removals = oldVds.Where(vds => newVds.All(nvds => nvds != vds)).ToArray();
            var additions = newVds.Where(nvds => oldVds.All(vds => vds != nvds)).ToArray();
            
            foreach(var removal in removals)
                listensToVdsChanges.ItemLeftVds(removal);
            
            // We do this in reverse, so the insertion callbacks get fired in the correct order. Otherwise, we'd insert as such.
            // The order is important, since we're doing this index based.
            // VDS : 3, 4, 5
            // Add : 0, 1, 2
            // Result : 2, 1, 0, 3, 4, 5
            int[] enumerableAdditions;
            if (additions.Any() && oldVds.Any() && additions.Last() < oldVds.First())
                enumerableAdditions = additions.Reverse().ToArray();
            else
                enumerableAdditions = additions.ToArray();

            foreach (var addition in enumerableAdditions)
            {
                var front = oldVds.Any() && addition < oldVds.First();
                listensToVdsChanges.ItemEnteredVds(addition, front);
            }
        }

        public static float CalculateInitialOffset(IEnumerable<int> visualDataSet, int entrySize, int spacing)
        {
            return visualDataSet.First() * (entrySize + spacing);
        }
    }
}