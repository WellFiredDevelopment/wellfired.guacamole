using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Adjacent
{
    [TestFixture]
    public class Given_AnAdjacentLayoutWithAdjecentLayoutChildren
    {
        [Test]
        public void When_TheFirstChildHasPadding_AndLayoutHappens_Then_LayoutIsPerformedSuccessfully()
        {
            var firstLabel = new Label {
                MinSize = UISize.Of(0, 10),
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalLayout = LayoutOptions.Fill,
                Text = "a"
            };

            var secondLabel = new Label {
                MinSize = UISize.Of(0, 10),
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalLayout = LayoutOptions.Fill,
                Text = "b"
            };

            var thirdLabel = new Label {
                MinSize = UISize.Of(0, 10),
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalLayout = LayoutOptions.Fill,
                Text = "c"
            };

            var fourthLabel = new Label {
                MinSize = UISize.Of(0, 10),
                HorizontalTextAlign = UITextAlign.Middle,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalLayout = LayoutOptions.Fill,
                Text = "d"
            };

            var firstEntry = new LayoutView {
                HorizontalLayout = LayoutOptions.Fill,
                Padding = UIPadding.With(0, 10, 0, 0),
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                Children = {
                    firstLabel,
                    secondLabel
                }
            };

            var secondEntry = new LayoutView {
                HorizontalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                Children = {
                    thirdLabel,
                    fourthLabel
                }
            };

            var view = new LayoutView {
                HorizontalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical),
                Children = {
                    firstEntry,
                    secondEntry
                }
            };
            
            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 300, 300));

            Assert.That(firstEntry.RectRequest.X, Is.EqualTo(0));
            Assert.That(firstEntry.RectRequest.Y, Is.EqualTo(0));
            Assert.That(firstEntry.RectRequest.Width, Is.EqualTo(300));
            Assert.That(firstEntry.RectRequest.Height, Is.EqualTo(20));
            
            Assert.That(secondEntry.RectRequest.X, Is.EqualTo(0));
            Assert.That(secondEntry.RectRequest.Y, Is.EqualTo(20));
            Assert.That(secondEntry.RectRequest.Width, Is.EqualTo(300));
            Assert.That(secondEntry.RectRequest.Height, Is.EqualTo(10));
        }
    }
}