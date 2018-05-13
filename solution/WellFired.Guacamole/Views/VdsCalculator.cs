using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Views
{
    public static class VdsCalculator
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static void CalculateVisualDataSetWithVariableHeight(float scrollOffset, float visibleControlSize, int maxEntries, Func<int, int> obtainHeight, ref List<int> visibleDataSet,
            out float initialOffset)
        {
            initialOffset = 0;
            
            if (MathUtil.NearEqual(Math.Abs(visibleControlSize), 0.0f))
                return;

            var focusedEntry = 0;
            var runningHeight = 0.0f;
            for (; focusedEntry < maxEntries; focusedEntry++)
            {
                var height = obtainHeight(focusedEntry);
                var start = runningHeight;
                var end = runningHeight + height;

                if (scrollOffset >= start && scrollOffset < end)
                {
                    initialOffset = start - scrollOffset;
                    break;
                }

                runningHeight += height;
            }

            var firstEntry = focusedEntry;
            var focusedHeight = obtainHeight(firstEntry) + initialOffset;
            
            visibleDataSet.Add(firstEntry);

            var runningTotal = focusedHeight;
            for (var index = firstEntry + 1; index < maxEntries; index++)
            {
                runningTotal += obtainHeight(index);
                
                visibleDataSet.Add(index);
                
                if (runningTotal >= visibleControlSize)
                    break;
            }
        }
        
        /// <summary>
        /// Given some data that defines a visible control, we can calculate a potentially visible data set, 
        /// this VDS will simply be a series of indicies into the data that are currently on visible.
        /// We calculate this data set using the params that define our view.
        /// </summary>
        /// <param name="virtualScrollPosition">Our Virtual Scroll position.</param>
        /// <param name="visibleControlSize">The visual size of the control on screen.</param>
        /// <param name="estimatedElementSize">The visual size of each individual element in the View.</param>
        /// <param name="estimatedContentSize">The visual total size of all of the content.</param>
        /// <param name="spacing"></param>
        /// <returns></returns>
        //todo : seems to be used only in tests, not in production code
        public static IEnumerable<int> CalculateVisualDataSet(float virtualScrollPosition, int visibleControlSize, int estimatedElementSize, int estimatedContentSize, int spacing)
        {
            var visibleDataSet = new List<int>();
     
            var deltaX = estimatedElementSize + spacing;
            var sizeX = visibleControlSize;
     
            var minIndex = (int)Math.Floor(virtualScrollPosition / deltaX);
            var variance = minIndex == 0 ? 0.1f : 1 / (float)minIndex;
            var maxIndex = (int)Math.Floor((virtualScrollPosition + sizeX) / (deltaX + variance)); // Here we add a small delta so we don't overun our buffer and select n + 1 visible elements
     
            var contentSizeX = estimatedContentSize;
            var max = (int)Math.Floor(contentSizeX * 2.0f / (deltaX + variance));

            var maxPossibleIndex = estimatedContentSize / estimatedElementSize - 1;
     
            if (minIndex < 0)
                minIndex = 0;
            if (maxIndex > max)
                maxIndex = max;
            if (maxIndex > maxPossibleIndex)
                maxIndex = maxPossibleIndex;
     
            for (var iterationIndex = minIndex; iterationIndex <= maxIndex; iterationIndex++)
                visibleDataSet.Add(iterationIndex);
     
            return visibleDataSet;
        }

        private static readonly List<int> Removals = new List<int>();
        private static readonly List<int> Additions = new List<int>();
        public static void AdjustForNewVds(List<int> oldVds, List<int> newVds, IListensToVdsChanges listensToVdsChanges)
        {
            Removals.Clear();
            Additions.Clear();

            for (var index = 0; index < oldVds.Count; index++)
            {
                var vds = oldVds[index];
                if(!newVds.Contains(vds))
                    Removals.Add(vds);
            }
            
            for (var index = 0; index < newVds.Count; index++)
            {
                var vds = newVds[index];
                if(!oldVds.Contains(vds))
                    Additions.Add(vds);
            }

            for (var index = 0; index < Removals.Count; index++)
            {
                var removal = Removals[index];
                listensToVdsChanges.ItemLeftVds(removal);
            }

            // We do this in reverse, so the insertion callbacks get fired in the correct order. Otherwise, we'd insert as such.
            // The order is important, since we're doing this index based.
            // VDS : 3, 4, 5
            // Add : 0, 1, 2
            // Result : 2, 1, 0, 3, 4, 5
            if (Additions.Any() && oldVds.Any() && Additions.Last() < oldVds.First())
            {
                for (var index = Additions.Count - 1; index >= 0; index--)
                {
                    listensToVdsChanges.ItemEnteredVds(Additions[index], true);       
                }
            }
            else
            {
                for (var index = 0; index < Additions.Count; index++)
                {
                    listensToVdsChanges.ItemEnteredVds(Additions[index], false);       
                }
            }
        }

        /// <summary>
        /// This will get the desired scroll for a specific item in the list.
        /// </summary>
        /// <returns></returns>
        public static float DesiredScrollFor(int dataIndex, int maxEntries, Func<int, int> obtainHeight)
        {
            var runningHeight = 0;
            for (var focusedEntry = 0; focusedEntry < dataIndex; focusedEntry++)
                runningHeight += obtainHeight(focusedEntry);

            return runningHeight;
        }
    }
}