using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Tests.Layout.Horizontal
{
    [TestFixture]
    public class Given_AViewWithAdjacentLayoutContentAndNoPadding
    {
        [Test]
        public void When_Layout_Then_LayoutIsCorrect()
        {
            var availableSize = UIRect.With(0, 0, 100, 100);

            // Mock out single child so that it's always the right size for this test.
            var child = Substitute.For<ILayoutable>();
            child.X.Returns(0);
            child.Y.Returns(0);
            child.RectRequest.Returns(UIRect.With(0, 0, 100, 100));

            // Mock our layout so we don't actually do any layouting.
            var layout = Substitute.For<ICanLayout>();
            layout.HorizontalLayout.Returns(LayoutOptions.Fill);
            layout.VerticalLayout.Returns(LayoutOptions.Fill);
            layout.Children.Returns(new List<ILayoutable> {child});
            layout.Layout = Substitute.For<ILayoutChildren>();
            layout.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.HorizontalLayout.Returns(LayoutOptions.Fill);
            parentView.VerticalLayout.Returns(LayoutOptions.Fill);
            parentView.Padding.Returns(UIPadding.Of(0));
            parentView.Content.Returns(layout);

            ViewSizingExtensions.DoSizingAndLayout(parentView, availableSize);

            Assert.That(parentView.RectRequest.X, Is.EqualTo(0));
            Assert.That(parentView.RectRequest.Y, Is.EqualTo(0));
            Assert.That(parentView.RectRequest.Width, Is.EqualTo(100));
            Assert.That(parentView.RectRequest.Height, Is.EqualTo(100));

            Assert.That(layout.RectRequest.X, Is.EqualTo(0));
            Assert.That(layout.RectRequest.Y, Is.EqualTo(0));
            Assert.That(layout.RectRequest.Width, Is.EqualTo(100));
            Assert.That(layout.RectRequest.Height, Is.EqualTo(100));
        }
    }
}