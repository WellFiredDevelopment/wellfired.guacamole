using System;
using System.Collections.Generic;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Layouts
{
    public class AdjacentLayout : ILayoutChildren
    {
        public OrientationOptions Orientation { get; set; }
        public int Spacing { get; set; }

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

        public UIRect CalculateValidRextRequest(IEnumerable<ILayoutable> layoutables, UISize minSize)
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

            return new UIRect(0, 0, Math.Max(totalWidth, minSize.Width), Math.Max(totalHeight, minSize.Height));
        }
    }
}