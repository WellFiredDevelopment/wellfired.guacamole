using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Layout.Horizontal
{
    [TestFixture]
    public class Given_AnAdjacentLayoutWithNoPadding_And_NoSpacing
    {
        [Test]
        public void OneChild()
        {
            var child = Substitute.For<ILayoutable>();
            child.X.Returns(0);
            child.Y.Returns(0);
            child.RectRequest.Returns(UIRect.With(0, 0, 50, 50));

            var layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal, Spacing = 0};
            layout.Layout(new [] { child }, UIPadding.Zero, LayoutOptions.Expand, LayoutOptions.Expand);

            Assert.That(child.X, Is.EqualTo(0));
            Assert.That(child.Y, Is.EqualTo(0));
        }

        [Test]
        public void TwoChildren()
        {
            var child = Substitute.For<ILayoutable>();
            child.X.Returns(0);
            child.Y.Returns(0);
            child.RectRequest.Returns(UIRect.With(0, 0, 50, 50));

            var child2 = Substitute.For<ILayoutable>();
            child2.X.Returns(0);
            child2.Y.Returns(0);
            child2.RectRequest.Returns(UIRect.With(0, 0, 50, 50));

            var layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal, Spacing = 0};
            layout.Layout(new [] { child, child2 }, UIPadding.Zero, LayoutOptions.Expand, LayoutOptions.Expand);

            Assert.That(child.X, Is.EqualTo(0));
            Assert.That(child.Y, Is.EqualTo(0));
            Assert.That(child2.X, Is.EqualTo(50));
            Assert.That(child2.Y, Is.EqualTo(0));
        }

        [Test]
        public void ThreeChildren()
        {
            var child = Substitute.For<ILayoutable>();
            child.X.Returns(0);
            child.Y.Returns(0);
            child.RectRequest.Returns(UIRect.With(0, 0, 50, 50));

            var child2 = Substitute.For<ILayoutable>();
            child2.X.Returns(0);
            child2.Y.Returns(0);
            child2.RectRequest.Returns(UIRect.With(0, 0, 50, 50));

            var child3 = Substitute.For<ILayoutable>();
            child3.X.Returns(0);
            child3.Y.Returns(0);
            child3.RectRequest.Returns(UIRect.With(0, 0, 50, 50));

            var layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal, Spacing = 0};
            layout.Layout(new [] { child, child2, child3 }, UIPadding.Zero, LayoutOptions.Expand, LayoutOptions.Expand);

            Assert.That(child.X, Is.EqualTo(0));
            Assert.That(child.Y, Is.EqualTo(0));
            Assert.That(child2.X, Is.EqualTo(50));
            Assert.That(child2.Y, Is.EqualTo(0));
            Assert.That(child3.X, Is.EqualTo(100));
            Assert.That(child3.Y, Is.EqualTo(0));
        }
    }
}