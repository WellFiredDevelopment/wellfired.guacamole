using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Simple
{
    public class Given_AView_WithPadding
    {
        [Test]
        public void That_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
                Padding = UIPadding.Of(10),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
            };

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 100)));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 100)));
        }

        [Test]
        public void That_HasASpecifiedMinSize_And_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
                Padding = UIPadding.Of(10),
                MinSize = UISize.Of(50, 10),
                MaxSize = UISize.Of(50, 10),
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
            };

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 500, 500));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 50, 10)));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 500, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 50, 10)));
        }
    }
}