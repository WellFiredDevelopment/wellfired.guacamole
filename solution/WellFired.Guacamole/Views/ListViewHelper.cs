using System;
using System.Linq;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;

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
            float space;
            switch (listView.Orientation)
            {
                case OrientationOptions.Vertical:
                    if (availableSpace.Height <= 0.0f)
                        return;
                    space = availableSpace.Height;
                    break;
                case OrientationOptions.Horizontal:
                    if (availableSpace.Width <= 0.0f)
                        return;
                    space = availableSpace.Width;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            listView.AvailableSpace = space;
            
            var isVertical = listView.Orientation == OrientationOptions.Vertical;
            var runningPosition = listView.InitialOffset;
            foreach (var child in listView.Children)
            {
                var entrySize = listView.GetEntrySizeFor(((View) child).BindingContext);
                
                if (isVertical)
                {
                    child.X = 0;
                    child.Y = runningPosition;

                    runningPosition += entrySize + listView.Spacing;
                }
                else
                {
                    child.X = runningPosition;
                    child.Y = 0;

                    runningPosition += entrySize + listView.Spacing;
                }
            }
        }

        public static float ClampScroll(float totalAvailableSpace, float totalContentSize, float value)
        {
            var maxValue = MaxScrollFor(totalAvailableSpace, totalContentSize);

            if (maxValue < 0.0f)
                maxValue = 0.0f;

            if (value < 0.0f)
                value = 0.0f;
            
            return value > maxValue ? maxValue : value;
        }

        public static float MaxScrollFor(float totalAvailableSpace, float totalContentSize)
        {
            return totalContentSize - totalAvailableSpace;
        }

        public static float CorrectScroll(OrientationOptions orientation, float value)
        {
            switch (orientation)
            {
                case OrientationOptions.Horizontal:
                    return value;
                case OrientationOptions.Vertical:
                    return value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public static void ConstrainToCell(IListView listView, ILayoutable child)
        {
            UIRect rectRequest;
            UIRect contentRectRequest;
            
            var entrySize = listView.GetEntrySizeFor(((View) child).BindingContext);
            
            switch (listView.Orientation)
            {
                case OrientationOptions.Horizontal:
                    rectRequest = child.RectRequest;
                    contentRectRequest = child.ContentRectRequest;
                    rectRequest.Width = entrySize + listView.Spacing;
                    contentRectRequest.Width = entrySize;
                    
                    rectRequest.Height = listView.RectRequest.Height;
                    contentRectRequest.Height = listView.RectRequest.Height;
                    break;
                case OrientationOptions.Vertical:
                    rectRequest = child.RectRequest;
                    contentRectRequest = child.ContentRectRequest;
                    rectRequest.Height = entrySize + listView.Spacing;
                    contentRectRequest.Height = entrySize;
                    
                    rectRequest.Width = listView.RectRequest.Width;
                    contentRectRequest.Width = listView.RectRequest.Width;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            child.RectRequest = rectRequest;
            child.ContentRectRequest = contentRectRequest;
        }
    }
}