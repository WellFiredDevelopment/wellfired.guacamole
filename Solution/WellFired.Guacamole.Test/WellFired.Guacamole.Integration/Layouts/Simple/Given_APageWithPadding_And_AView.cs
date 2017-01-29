using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layout.Simple
{
    public class Given_APageWithPadding_And_AView
    {
        [Test]
        public void That_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
            };

            var page = new Page {
                Padding = UIPadding.Of(10),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Content = view
            };

            ViewSizingExtensions.DosizingAndLayout(page, UIRect.With(100, 100));

            Assert.That(page.RectRequest, Is.EqualTo(UIRect.With(100, 100)));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(10, 10, 80, 80)));

            ViewSizingExtensions.DosizingAndLayout(page, UIRect.With(100, 100));

            Assert.That(page.RectRequest, Is.EqualTo(UIRect.With(100, 100)));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(10, 10, 80, 80)));
        }

        [Test]
        public void That_HasASpecifiedMinSize_And_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
                MinSize = UISize.Of(50, 10),
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
            };

            var page = new Page {
                Padding = UIPadding.Of(10),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Content = view
            };

            ViewSizingExtensions.DosizingAndLayout(page, UIRect.With(500, 500));

            Assert.That(page.RectRequest, Is.EqualTo(UIRect.With(500, 500)));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(10, 10, 50, 10)));

            ViewSizingExtensions.DosizingAndLayout(page, UIRect.With(500, 500));

            Assert.That(page.RectRequest, Is.EqualTo(UIRect.With(500, 500)));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(10, 10, 50, 10)));
        }
    }
}