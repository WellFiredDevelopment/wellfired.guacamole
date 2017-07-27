using System;
using System.Linq;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
    public static class ListViewHelper
    {
        public static UIRect CalculateValidRectRequest(IListView listView)
        {
            var size = listView.TotalContentSize;
            var request = new UIRect();
            switch (listView.Orientation)
            {
                case OrientationOptions.Horizontal:
                    request.Width = size;
                    request.Height = !listView.Children.Any() ? 0 : listView.Children[0].RectRequest.Height + listView.ScrollBarSize;
                    break;
                case OrientationOptions.Vertical:
                    request.Height = size;
                    request.Width = (!listView.Children.Any() ? 0 : listView.Children[0].RectRequest.Width) + listView.ScrollBarSize;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return request;
        }

        public static void Layout(IListView listView, UIRect availableSpace, UIPadding containerPadding)
        {
            var isVertical = listView.Orientation == OrientationOptions.Vertical;
            var runningPosition = listView.InitialOffset;
            foreach (var child in listView.Children)
            {
                if (isVertical)
                    child.Y = (int)runningPosition;
                else
                    child.X = (int)runningPosition;

                runningPosition += listView.EntrySize + listView.Spacing;
            }

            int entriesThatCanBeShownAtOnce;
            switch (listView.Orientation)
            {
                case OrientationOptions.Vertical:
                    if (availableSpace.Height <= 0.0f)
                        return;
                    entriesThatCanBeShownAtOnce = (int)Math.Ceiling((float)availableSpace.Height / listView.EntrySize);
                    break;
                case OrientationOptions.Horizontal:
                    if (availableSpace.Width <= 0.0f)
                        return;
                    entriesThatCanBeShownAtOnce = (int)Math.Ceiling((float)availableSpace.Width / listView.EntrySize);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            listView.NumberOfVisibleEntries = entriesThatCanBeShownAtOnce;
        }

        public static float ClampScroll(IListView listView, float value)
        {
            if (value > 0.0f)
                return 0.0f;
            var maxValue = MaxScrollFor(listView);
            if (value < -maxValue)
                return -maxValue;

            return value;
        }

        public static float MaxScrollFor(IListView listView)
        {
            var visibleSizeWithSpacing = (listView.NumberOfVisibleEntries - 1) * listView.Spacing;
            return listView.TotalContentSize - (listView.NumberOfVisibleEntries * listView.EntrySize + visibleSizeWithSpacing);
        }

        public static float CorrectScroll(OrientationOptions orientation, float value)
        {
            switch (orientation)
            {
                case OrientationOptions.Horizontal:
                    return value;
                case OrientationOptions.Vertical:
                    return -value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public static void ConstrainToCell(IListView listView, ILayoutable child)
        {
            UIRect rectRequest;
            UIRect contentRectRequest;
            switch (listView.Orientation)
            {
                case OrientationOptions.Horizontal:
                    rectRequest = child.RectRequest;
                    contentRectRequest = child.ContentRectRequest;
                    rectRequest.Width = listView.EntrySize + listView.Spacing;
                    contentRectRequest.Width = listView.EntrySize;
                    break;
                case OrientationOptions.Vertical:
                    rectRequest = child.RectRequest;
                    contentRectRequest = child.ContentRectRequest;
                    rectRequest.Height = listView.EntrySize + listView.Spacing;
                    contentRectRequest.Height = listView.EntrySize;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            child.RectRequest = rectRequest;
            child.ContentRectRequest = contentRectRequest;
        }
    }
}