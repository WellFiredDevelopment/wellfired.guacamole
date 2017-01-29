using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layout.Simple
{
    public class Given_AWindowWithContent
    {
        [Test]
        public void That_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = new Slider {
                MinSize = UISize.Of(50, 10),
                MinValue = 0.0,
                MaxValue = 10.0,
                Value = 5.0
            };

            var window = new Window {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Content = view
            };

            window.Layout(UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 10)));

            window.Layout(UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 10)));
        }

        [Test]
        public void That_HasASpecifiedMinSize_And_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = new Slider {
                MinSize = UISize.Of(50, 10),
                MinValue = 0.0,
                MaxValue = 10.0,
                Value = 5.0
            };

            var window = new Window {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Content = view
            };

            ViewSizingExtensions.DosizingAndLayout(window, UIRect.With(0, 0, 500, 500));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 500, 10)));

            ViewSizingExtensions.DosizingAndLayout(window, UIRect.With(0, 0, 500, 500));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 500, 10)));
        }
    }
}