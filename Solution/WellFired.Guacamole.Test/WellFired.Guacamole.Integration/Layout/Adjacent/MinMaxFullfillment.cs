using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layout.Adjacent
{
    [TestFixture]
    public class MinMaxFullfillment
    {
        [Test]
        public void LayoutWithNoChildrenAndMinSize()
        {
            var adjacentLayout = new LayoutView
            {
                Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
                Spacing = 0,
                MinSize = new UISize(100, 100)
            };

            adjacentLayout.CalculateRectRequest();
            adjacentLayout.AttemptToFullfillRequests(new UIRect(0, 0, 500, 500));
            adjacentLayout.UpdateContextIfNeeded();

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(100));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(100));
        }

        [Test]
        public void LayoutWithOneChildAndMinSizeNoOverflow()
        {
            var adjacentLayout = new LayoutView
            {
                Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
                Spacing = 0,
                MinSize = new UISize(100, 100),
                Children =
                {
                    new Label { MinSize = new UISize(50, 50) }
                }
            };

            adjacentLayout.CalculateRectRequest();
            adjacentLayout.AttemptToFullfillRequests(new UIRect(0, 0, 500, 500));
            adjacentLayout.UpdateContextIfNeeded();

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(100));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(100));
        }

        [Test]
        public void HorizontalLayoutWithMultipleChildrenAndMinSizeOverflow()
        {
            var adjacentLayout = new LayoutView
            {
                Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
                MinSize = new UISize(100, 100),
                Spacing = 0,
                Children =
                {
                    new Label { MinSize = new UISize(50, 50) },
                    new Label { MinSize = new UISize(50, 50) },
                    new Label { MinSize = new UISize(50, 50) }
                }
            };

            adjacentLayout.CalculateRectRequest();
            adjacentLayout.AttemptToFullfillRequests(new UIRect(0, 0, 500, 500));
            adjacentLayout.UpdateContextIfNeeded();

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(150));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(100));
        }

        [Test]
        public void VerticalLayoutWithMultipleChildrenAndMinSizeOverflow()
        {
            var adjacentLayout = new LayoutView
            {
                Layout = new AdjacentLayout { Orientation = OrientationOptions.Vertical },
                MinSize = new UISize(100, 100),
                Children =
                {
                    new Label { MinSize = new UISize(50, 50) },
                    new Label { MinSize = new UISize(50, 50) },
                    new Label { MinSize = new UISize(50, 50) }
                }
            };

            adjacentLayout.CalculateRectRequest();
            adjacentLayout.AttemptToFullfillRequests(new UIRect(0, 0, 500, 500));
            adjacentLayout.UpdateContextIfNeeded();

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(100));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(150));
        }

    }
}