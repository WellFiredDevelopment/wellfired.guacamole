using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    internal static class ListViewHelper
    {
        public static UIRect CalculateValidRectRequest(IListView listView)
        {
            var size = listView.TotalContentSize;
            var request = new UIRect();
            switch (listView.Orientation)
            {
                case OrientationOptions.Horizontal:
                    request.Width = size;
                    break;
                case OrientationOptions.Vertical:
                    request.Height = size;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return request;
        }

        public static void Layout(IListView listView, UIRect availableSpace, UIPadding containerPadding)
        {
            var isVertical = listView.Orientation == OrientationOptions.Vertical;
            var runningPosition = 0;
            foreach (var child in listView.Children)
            {
                if (isVertical)
                    child.Y = runningPosition;
                else
                    child.X = runningPosition;

                runningPosition += (listView.EntrySize + listView.Spacing);
            }
        }
    }
}