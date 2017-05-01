using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Complex
{
    [TestFixture]
    public class Given_AnAdjacentLayout_WithAComplexSetup
    {
        [Test]
        public void When_Layout_Then_LayoutIsCorrect()
        {
            var horizontalLayout = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Spacing = 0,
                Children =
                {
                    new Label { HorizontalLayout = LayoutOptions.Fill, VerticalLayout = LayoutOptions.Fill },
                    new Label { HorizontalLayout = LayoutOptions.Fill, VerticalLayout = LayoutOptions.Fill }
                }
            };

            var label = new Label {HorizontalLayout = LayoutOptions.Fill, MinSize = UISize.Of(500, 50)};

            var verticalLayout = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical),
                Spacing = 0,
                Children =
                {
                    label,
                    horizontalLayout
                }
            };

            ViewSizingExtensions.DoSizingAndLayout(verticalLayout, UIRect.With(500, 500));

            var verticalRequest = verticalLayout.RectRequest;
            Assert.That(verticalRequest, Is.EqualTo(UIRect.With(0, 0, 500, 500)));

            var labelRequest = label.RectRequest;
            Assert.That(labelRequest, Is.EqualTo(UIRect.With(0, 0, 500, 50)));

            var horizontalRequest = horizontalLayout.RectRequest;
            Assert.That(horizontalRequest, Is.EqualTo(UIRect.With(0, 50, 500, 450)));

            var horizontalChild1Request = horizontalLayout.Children[0].RectRequest;
            Assert.That(horizontalChild1Request, Is.EqualTo(UIRect.With(0, 0, 250, 450)));

            var horizontalChild2Request = horizontalLayout.Children[1].RectRequest;
            Assert.That(horizontalChild2Request, Is.EqualTo(UIRect.With(250, 0, 250, 450)));
        }
    }
}