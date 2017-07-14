using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Layout
{
    public static class LayoutFactory
    {
        public static LayoutView CreateVerticalLayout(params ILayoutable[] children)
        {
            return new LayoutView
            {
                BackgroundColor = UIColor.Grey,
                Layout = new AdjacentLayout {Orientation = OrientationOptions.Vertical},
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Expand,
                Children = children
            };
        }

        public static LayoutView CreateHorizontalLayout(params ILayoutable[] children)
        {
            return new LayoutView
            {
                BackgroundColor = UIColor.Grey,
                Layout = new AdjacentLayout {Orientation = OrientationOptions.Horizontal},
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Expand,
                Children = children
            };
        }
    }
}