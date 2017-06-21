using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Layout.Vertical
{
    [TestFixture]
    public class Given_AnAdjacentLayoutWithNoSpacing
    {
        [Test]
        public void OneChild()
        {
            var child = Substitute.For<ILayoutable>();
            child.X.Returns(0);
            child.Y.Returns(0);
            child.RectRequest.Returns(UIRect.With(0, 0, 50, 50));

            var layout = new AdjacentLayout { Orientation = OrientationOptions.Vertical, Spacing = 0 };
            layout.Layout(new [] { child }, UIRect.Max, UIPadding.Of(10));

            Assert.That(child.X, Is.EqualTo(10));
            Assert.That(child.Y, Is.EqualTo(10));
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

            var layout = new AdjacentLayout { Orientation = OrientationOptions.Vertical, Spacing = 0};
            layout.Layout(new [] { child, child2 }, UIRect.Max, UIPadding.Of(10));

            Assert.That(child.X, Is.EqualTo(10));
            Assert.That(child.Y, Is.EqualTo(10));
            Assert.That(child2.X, Is.EqualTo(10));
            Assert.That(child2.Y, Is.EqualTo(60));
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

            var layout = new AdjacentLayout { Orientation = OrientationOptions.Vertical, Spacing = 0};
            layout.Layout(new [] { child, child2, child3 }, UIRect.Max, UIPadding.Of(10));

            Assert.That(child.X, Is.EqualTo(10));
            Assert.That(child.Y, Is.EqualTo(10));
            Assert.That(child2.X, Is.EqualTo(10));
            Assert.That(child2.Y, Is.EqualTo(60));
            Assert.That(child3.X, Is.EqualTo(10));
            Assert.That(child3.Y, Is.EqualTo(110));
        }
    }
}