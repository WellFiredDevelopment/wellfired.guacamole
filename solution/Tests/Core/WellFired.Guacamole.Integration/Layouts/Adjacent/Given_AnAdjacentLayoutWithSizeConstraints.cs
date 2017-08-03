using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Adjacent
{
    [TestFixture]
    public class Given_AnAdjacentLayoutWithSizeConstraints
    {
        [Test]
        public void And_NoChildren_And_MinSize_When_Layout_Then_LayoutIsCorrect()
        {
            var adjacentLayout = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                MinSize = UISize.Of(100)
            };

            ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(100));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(100));
        }

        [Test]
        public void And_OneChild_And_MinSize_When_Layout_Then_LayoutIsCorrect()
        {
            var adjacentLayout = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                MinSize = UISize.Of(100),
                Children =
                {
                    new Label { MinSize = UISize.Of(50) }
                }
            };

            ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(100));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(100));
        }

        [Test]
        public void And_MultipleChildren_And_MinSize_When_Layout_Then_LayoutIsCorrect()
        {
            var adjacentLayout = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                MinSize = UISize.Of(100),
                Children =
                {
                    new Label { MinSize = UISize.Of(50) },
                    new Label { MinSize = UISize.Of(50) },
                    new Label { MinSize = UISize.Of(50) }
                }
            };

            ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(150));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(100));
        }
    }
}