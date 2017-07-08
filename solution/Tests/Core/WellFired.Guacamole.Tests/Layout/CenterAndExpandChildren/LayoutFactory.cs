using NSubstitute;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Layout.CenterAndExpandChildren
{
    internal static class LayoutFactory
    {
        // For the duration of Horizontal tests, we will use a parent view of 0, 0, 200, 60, 
        // hopefully this gives us easy to break child sizes.
        private static readonly UIRect HorizontalFixedAvailableSize = UIRect.With(0, 0, 200, 60);
        private static readonly UIRect VerticalFixedAvailableSize = UIRect.With(0, 0, 60, 200);
        
        // Child Fixed Size
        private static readonly UIRect ChildFixedSize = UIRect.With(0, 0, 20, 20);

        public static ILayoutable ChildWithHEVC()
        {
            var child = Substitute.For<ILayoutable>();
            child.RectRequest.Returns(ChildFixedSize);
            child.HorizontalLayout = LayoutOptions.Expand;
            child.VerticalLayout = LayoutOptions.Center;
            return child;
        }
        
        public static ILayoutable ChildWithHCVE()
        {
            var child = Substitute.For<ILayoutable>();
            child.RectRequest.Returns(ChildFixedSize);
            child.HorizontalLayout = LayoutOptions.Center;
            child.VerticalLayout = LayoutOptions.Expand;
            return child;
        }

        public static ILayoutable ChildWithHEVE()
        {
            var child = Substitute.For<ILayoutable>();
            child.RectRequest.Returns(ChildFixedSize);
            child.HorizontalLayout = LayoutOptions.Expand;
            child.VerticalLayout = LayoutOptions.Expand;
            return child;   
        }

        public static ILayoutable ChildWithHCVC()
        {
            var child = Substitute.For<ILayoutable>();
            child.RectRequest.Returns(ChildFixedSize);
            child.HorizontalLayout = LayoutOptions.Center;
            child.VerticalLayout = LayoutOptions.Center;
            return child;   
        }

        public static void HorizontalLayoutWith(params ILayoutable[] children)
        {
            AdjacentLayout.Of(OrientationOptions.Horizontal).Layout(children, HorizontalFixedAvailableSize, UIPadding.Zero);
        }

        public static void VerticalLayoutWith(params ILayoutable[] children)
        {
            AdjacentLayout.Of(OrientationOptions.Vertical).Layout(children, VerticalFixedAvailableSize, UIPadding.Zero);
        }
    }
}