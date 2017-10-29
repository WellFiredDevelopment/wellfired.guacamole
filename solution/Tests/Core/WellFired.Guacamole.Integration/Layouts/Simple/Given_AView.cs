using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Simple
{
    public class GivenAView
    {
        [Test]
        public void That_IsHCVE_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
                MinSize = UISize.Of(20),
                HorizontalLayout = LayoutOptions.Center,
                VerticalLayout = LayoutOptions.Expand,
            };

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 20)));
            Assert.That(view.ContentRectRequest, Is.EqualTo(UIRect.With(40, 0, 20, 20)));
        }
        
        [Test]
        public void That_IsHEVC_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
                MinSize = UISize.Of(20),
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Center,
            };

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 20, 100)));
            Assert.That(view.ContentRectRequest, Is.EqualTo(UIRect.With(0, 40, 20, 20)));
        }
        [Test]
        public void That_IsHCVF_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
                MinSize = UISize.Of(20),
                HorizontalLayout = LayoutOptions.Center,
                VerticalLayout = LayoutOptions.Fill,
            };

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 100)));
            Assert.That(view.ContentRectRequest, Is.EqualTo(UIRect.With(40, 0, 20, 100)));
        }
        
        [Test]
        public void That_IsHFVC_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
                MinSize = UISize.Of(20),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Center,
            };

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 100)));
            Assert.That(view.ContentRectRequest, Is.EqualTo(UIRect.With(0, 40, 100, 20)));
        }
        
        [Test]
        public void That_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = new Views.View {
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
                MinSize = UISize.Of(50, 10),
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