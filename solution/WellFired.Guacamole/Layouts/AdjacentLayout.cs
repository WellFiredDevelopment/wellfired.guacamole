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
        public LayoutOptions VerticalLayout { get; set; }
        public LayoutOptions HorizontalLayout { get; set; }

        public void Layout(ICollection<ILayoutable> layoutables, UIRect availableSpace, UIPadding containerPadding)
        {
            var x = containerPadding.Left;
            var y = containerPadding.Top;
            
            if (HorizontalLayout == LayoutOptions.Center)
            {
                var totalWidth = layoutables.Sum(o => o.RectRequest.Width) / 2;
                var totalWidthWithSpacing = totalWidth + (layoutables.Count - 1) * Spacing / 2;
                x = availableSpace.Width / 2 - totalWidthWithSpacing;
            }
            if (VerticalLayout == LayoutOptions.Center)
            {
                var totalHeight = layoutables.Sum(o => o.RectRequest.Height) / 2;
                var totalHeightWithSpacing = totalHeight + (layoutables.Count - 1) * Spacing / 2;
                y = availableSpace.Height / 2 - totalHeightWithSpacing;
            }
            
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

        public void AttemptToFullfillRequests(ICollection<ILayoutable> layoutables, UIRect availableSpace, UIPadding containerPadding, LayoutOptions horizontalLayout, LayoutOptions verticalLayout)
        {
            AdjacentLayoutCellCalculator.Calculate(layoutables, availableSpace, Orientation, Spacing);
            
            foreach (var layoutable in layoutables)
                ViewSizingExtensions.AttemptToFullfillRequests(layoutable as IView, layoutable.RectRequest);
        }

        public static ILayoutChildren Of(OrientationOptions orientation)
        {
            return new AdjacentLayout { Orientation = orientation };
        }

        public static ILayoutChildren Of(OrientationOptions orientation, int spacing)
        {
            return new AdjacentLayout { Orientation = orientation, Spacing = spacing };
        }

        public static ILayoutChildren Of(OrientationOptions orientation, int spacing, LayoutOptions horizontalLayoutOptions, LayoutOptions verticalLayoutOptions)
        {
            return new AdjacentLayout { Orientation = orientation, Spacing = spacing, HorizontalLayout = horizontalLayoutOptions, VerticalLayout = verticalLayoutOptions };
        }
    }
}