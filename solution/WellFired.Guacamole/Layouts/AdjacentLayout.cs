using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Layouts
{
    public class AdjacentLayout : ILayoutChildren
    {
        public OrientationOptions Orientation { private get; set; }
        public int Spacing { private get; set; }

        public void Layout(IEnumerable<ILayoutable> layoutables, UIPadding containerPadding, LayoutOptions containerHorizontalLayoutOptions, LayoutOptions containerVerticalLayoutOptions)
        {
            var x = containerPadding.Left;
            var y = containerPadding.Top;
            switch (Orientation)
            {
                case OrientationOptions.Horizontal:
                    foreach (var layoutable in layoutables)
                    {
                        layoutable.X = x;
                        layoutable.Y = y;
                        x += layoutable.RectRequest.Width + Spacing;
                    }
                    break;
                case OrientationOptions.Vertical:
                    foreach (var layoutable in layoutables)
                    {
                        layoutable.X = x;
                        layoutable.Y = y;
                        y += layoutable.RectRequest.Height + Spacing;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public UIRect CalculateValidRectRequest(IEnumerable<ILayoutable> layoutables, UISize minSize)
        {
            var totalWidth = 0;
            var totalHeight = 0;
            foreach (var layoutable in layoutables)
            {
                var size = layoutable.RectRequest;

                switch (Orientation)
                {
                    case OrientationOptions.Horizontal:
                        totalWidth += size.X + size.Width + Spacing;
                        totalHeight = Math.Max(totalHeight, size.Height);
                        break;
                    case OrientationOptions.Vertical:
                        totalHeight += size.Y + size.Height + Spacing;
                        totalWidth = Math.Max(totalWidth, size.Width);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            // This is done seperately as a final step, since we could potentially have hundreds of elements in our layout
            switch (Orientation)
            {
                case OrientationOptions.Horizontal:
                    totalWidth -= Spacing;
                    break;
                case OrientationOptions.Vertical:
                    totalHeight -= Spacing;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return UIRect.With(0, 0, Math.Max(totalWidth, minSize.Width), Math.Max(totalHeight, minSize.Height));
        }

        public void AttemptToFullfillRequests(IList<ILayoutable> children, UIRect availableSpace, UIPadding containerPadding, LayoutOptions horizontalLayout, LayoutOptions verticalLayout)
        {

            switch (Orientation)
            {
                case OrientationOptions.Horizontal:
                    var horizontalDynamicChildren = children.Where(child => (child as View).HorizontalLayout == LayoutOptions.Fill);
                    var dynamicChildren = horizontalDynamicChildren as View[] ?? horizontalDynamicChildren.ToArray();
                    var horizontalStaticChildren = children.Except(dynamicChildren);
                    var staticChildren = horizontalStaticChildren as View[] ?? horizontalStaticChildren.ToArray();
                    var staticWidth = staticChildren.Sum(child => child.RectRequest.Width);
                    var sharedWidth = !dynamicChildren.Any()
                        ? 0
                        : (availableSpace.Width - staticWidth - containerPadding.Width - Spacing*(children.Count - 1))/dynamicChildren.Length;

                    // This is just to stop the UI from looking weird as hell if the user shrinks the UI too much.
                    if (sharedWidth < 0)
                        sharedWidth = 0;

                    var newHeight = availableSpace.Height;
                    if (verticalLayout == LayoutOptions.Fill)
                        newHeight = availableSpace.Height - containerPadding.Height;

                    foreach (var child in dynamicChildren)
                    {
                        var sharedAvailableSpace = UIRect.With(
                            availableSpace.X,
                            availableSpace.Y,
                            sharedWidth,
                            newHeight);
                        ViewSizingExtensions.AttemptToFullfillRequests(child as IView, sharedAvailableSpace);
                    }
                    foreach (var child in staticChildren)
                    {
                        var staticAvailableSpace = UIRect.With(
                            availableSpace.X,
                            availableSpace.Y,
                            child.RectRequest.Width,
                            newHeight);
                        ViewSizingExtensions.AttemptToFullfillRequests(child as IView, staticAvailableSpace);
                    }
                    break;
                case OrientationOptions.Vertical:
                    var verticalDynamicChildren = children.Where(child => (child as View).VerticalLayout == LayoutOptions.Fill);
                    var viewBases = verticalDynamicChildren as View[] ?? verticalDynamicChildren.ToArray();
                    var verticalStaticChildren = children.Except(viewBases);
                    var enumerable = verticalStaticChildren as View[] ?? verticalStaticChildren.ToArray();
                    var staticHeight = enumerable.Sum(child => child.RectRequest.Height);
                    var sharedHeight = !viewBases.Any()
                        ? 0
                        : (availableSpace.Height - containerPadding.Height - Spacing*(children.Count - 1) - staticHeight)/viewBases.Length;

                    // This is just to stop the UI from looking weird as hell if the user shrinks the UI too much.
                    if (sharedHeight < 0)
                        sharedHeight = 0;

                    var newWidth = availableSpace.Width;
                    if (horizontalLayout == LayoutOptions.Fill)
                        newWidth = availableSpace.Width - containerPadding.Width;

                    foreach (var child in viewBases)
                    {
                        var sharedAvailableSpace = UIRect.With(
                            availableSpace.X,
                            availableSpace.Y,
                            newWidth,
                            sharedHeight);
                        ViewSizingExtensions.AttemptToFullfillRequests(child as IView, sharedAvailableSpace);
                    }
                    foreach (var child in enumerable)
                    {
                        var staticAvailableSpace = UIRect.With(
                            availableSpace.X,
                            availableSpace.Y,
                            newWidth,
                            child.RectRequest.Height);
                        ViewSizingExtensions.AttemptToFullfillRequests(child as IView, staticAvailableSpace);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static ILayoutChildren Of(OrientationOptions orientation)
        {
            return new AdjacentLayout { Orientation = orientation };
        }
    }
}