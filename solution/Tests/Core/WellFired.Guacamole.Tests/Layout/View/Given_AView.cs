using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Tests.Layout.View
{
    [TestFixture]
    public class Given_AView
    {
        [Test]
        public void That_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = Substitute.For<IView>();
            view.HorizontalLayout.Returns(LayoutOptions.Fill);
            view.VerticalLayout.Returns(LayoutOptions.Fill);
            view.Content.Returns(default(IView));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 100)));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 100)));
        }

        [Test]
        public void That_HasASpecifiedMinSize_And_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = Substitute.For<IView>();
            view.HorizontalLayout.Returns(LayoutOptions.Expand);
            view.VerticalLayout.Returns(LayoutOptions.Expand);
            view.MinSize.Returns(UISize.Of(50, 10));
            view.Content.Returns(default(IView));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 500, 500));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 50, 10)));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 500, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 50, 10)));
        }
    }
}