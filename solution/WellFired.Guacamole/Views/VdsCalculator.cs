using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Views
{
    public static class VdsCalculator
    {
        public static void CalculateVisualDataSet(float scrollOffset, float visibleControlSize, CompositeCollection collection, float headerSize, float entrySize, 
	        ref List<int> visibleDataSet, out float initialOffset)
        {
            initialOffset = 0;
			
			if (collection.IsContiguousCollection)
			{
				CalculateForContiguousCollection(collection, scrollOffset, visibleControlSize, entrySize, visibleDataSet, out initialOffset);
			}
			else
			{
				CalculateForNonContiguousCollection(collection, scrollOffset, visibleControlSize, headerSize, entrySize, visibleDataSet, out initialOffset);
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

	    private static void CalculateForContiguousCollection(CompositeCollection collection, float scrollOffset, float visibleControlSize, float entrySize, List<int> visibleDataSet, out float initialOffset)
	    {
		    initialOffset = GetOffsetOfFirstVisibleItem(scrollOffset, entrySize, out var firstItem);

		    var numberOfVisibleItem = Math.Ceiling(visibleControlSize / entrySize);
		    for (var i = 0; i < numberOfVisibleItem; i++)
		    {
			    if (firstItem + i >= collection.Count)
				    break;

			    visibleDataSet.Add(firstItem + i);
		    }
	    }

	    /// <summary>
	    /// We use the composite collection utility functions to find what is the first visible group in our composite collecction.
	    /// Then we keep going through groups to find out which one is the last visible and which of its element is the last
	    /// visible one. This function allowed pretty good performance to scroll grouped list view, but it should be noted that
	    /// the more group the list view has, the slower it will be.
	    /// </summary>
	    /// <param name="collection"></param>
	    /// <param name="scrollOffset"></param>
	    /// <param name="visibleControlSize"></param>
	    /// <param name="headerSize"></param>
	    /// <param name="entrySize"></param>
	    /// <param name="visibleDataSet"></param>
	    /// <param name="initialOffset"></param>
	    private static void CalculateForNonContiguousCollection(CompositeCollection collection, float scrollOffset, float visibleControlSize, float headerSize, float entrySize, List<int> visibleDataSet, out float initialOffset)
	    {
		    var runningHeight = 0f;
		    var group = 0;
		    var firstVisibleEntry = -1;
		    float groupEndPosition;
		    initialOffset = 0;
		    var firstVisibleItemIndex = 0;

		    for (; group < collection.GroupCount; group++)
		    {
			    var start = runningHeight + headerSize;

			    //only the bottom part of the header is inside our control view
			    if (start > scrollOffset)
			    {
				    initialOffset = -(scrollOffset - runningHeight);
				    break;
			    }

			    var entryCountInGroup = collection.GetEntryCountInGroup(group);

			    groupEndPosition = start + entryCountInGroup * entrySize;

			    //only the bottom part of the group (excluding header) is inside our control view
			    if (groupEndPosition >= scrollOffset)
			    {
				    initialOffset = GetOffsetOfFirstVisibleItem(scrollOffset - start, entrySize, out firstVisibleEntry);
				    firstVisibleItemIndex += firstVisibleEntry + 1;
				    break;
			    }

			    //The whole group is above the visible part of our control view.
			    
			    firstVisibleItemIndex += entryCountInGroup + 1;
			    runningHeight = groupEndPosition;
		    }

		    var visibleHeight = 0f;
		    var visibleItemCount = 0;
		    var offsetToRemove = -initialOffset;
		    while (visibleHeight < visibleControlSize && group < collection.GroupCount)
		    {
			    //The visible part of our control starts with an entry partly visible
			    if (firstVisibleEntry > -1)
			    {
				    visibleHeight = entrySize - offsetToRemove;
				    offsetToRemove = 0;
				    visibleItemCount++;

				    var remainingItem = collection.GetEntryCountInGroup(group) - firstVisibleEntry - 1;
				    groupEndPosition = visibleHeight + remainingItem * entrySize;

				    //The group ends below the visible part of our control
				    if (groupEndPosition >= visibleControlSize)
				    {
					    var numberOfRemainingVisibleItem = Math.Ceiling((visibleControlSize - visibleHeight) / entrySize);

					    visibleItemCount += (int) numberOfRemainingVisibleItem;
					    break;
				    }

				    //Another group below the current one is also visible.
				    visibleHeight = groupEndPosition;
				    visibleItemCount += remainingItem;
				    firstVisibleEntry = -1;
				    group++;

				    continue;
			    }

			    //The visible height we are at in the visible part of our control starts with a group header.
			    visibleHeight += headerSize - offsetToRemove;
			    offsetToRemove = 0;
			    visibleItemCount++;

			    if (visibleHeight < visibleControlSize)
			    {
				    var entryCountInGroup = collection.GetEntryCountInGroup(group);
				    
				    groupEndPosition = visibleHeight + entryCountInGroup * entrySize;

				    if (groupEndPosition >= visibleControlSize)
				    {
					    var numberOfRemainingVisibleItem = Math.Ceiling((visibleControlSize - visibleHeight) / entrySize);

					    visibleItemCount += (int) numberOfRemainingVisibleItem;

					    break;
				    }

				    visibleHeight = groupEndPosition;
				    visibleItemCount += entryCountInGroup;
				    group++;
			    }
		    }

		    for (var i = 0; i < visibleItemCount; i++)
		    {
			    if (firstVisibleItemIndex + i >= collection.Count)
				    break;

			    visibleDataSet.Add(firstVisibleItemIndex + i);
		    }
	    }
	    
	    /// <summary>
	    /// Return offset of the first visible item.
	    /// </summary>
	    /// <param name="invisibleAreaSize">Size of the area hidding the entries</param>
	    /// <param name="entriesSize">size of entries</param>
	    /// <param name="firstVisibleEntryIndex">index from 0 of the first visible entry</param>
	    /// <returns></returns>
	    private static float GetOffsetOfFirstVisibleItem(float invisibleAreaSize, float entriesSize, out int firstVisibleEntryIndex)
	    {
		    var itemCount = invisibleAreaSize / entriesSize;
		    firstVisibleEntryIndex = (int) Math.Truncate(itemCount);
		    var decimalPart = itemCount - firstVisibleEntryIndex;

		    return -(decimalPart * entriesSize);
	    }

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