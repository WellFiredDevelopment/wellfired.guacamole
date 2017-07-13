using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.AdjacentLayout.CalculateValidRectRequest
{
    [TestFixture]
    public class Given_AnHorizontalAdacentLayoutWithFiveSpacing
    {
        [Test]
        public void With_OneChild_And_NoMinSize()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0}, UISize.Zero);

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 10, 10)));
        }
        
        [Test]
        public void With_TwoChildren_And_NoMinSize()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child1 = Substitute.For<ILayoutable>();
            child1.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0, child1}, UISize.Zero);

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 25, 10)));
        }
        
        [Test]
        public void With_ThreeChildren_And_NoMinSize()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child1 = Substitute.For<ILayoutable>();
            child1.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child2 = Substitute.For<ILayoutable>();
            child2.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0, child1, child2}, UISize.Zero);

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 40, 10)));
        }
        
        [Test]
        public void With_OneChild_And_MinSizeThatTakesUpLessSpace()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0}, UISize.Of(3));

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 10, 10)));
        }
        
        [Test]
        public void With_TwoChildren_And_MinSizeThatTakesUpLessSpace()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child1 = Substitute.For<ILayoutable>();
            child1.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0, child1}, UISize.Of(3));

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 25, 10)));
        }
        
        [Test]
        public void With_ThreeChildren_And_MinSizeThatTakesUpLessSpace()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child1 = Substitute.For<ILayoutable>();
            child1.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child2 = Substitute.For<ILayoutable>();
            child2.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0, child1, child2}, UISize.Of(3));

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 40, 10)));
        }
        
        [Test]
        public void With_OneChild_And_MinSizeThatTakesUpMoreSpace()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0}, UISize.Of(300));

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 300, 300)));
        }
        
        [Test]
        public void With_TwoChildren_And_MinSizeThatTakesUpMoreSpace()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child1 = Substitute.For<ILayoutable>();
            child1.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0, child1}, UISize.Of(300));

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 300, 300)));
        }
        
        [Test]
        public void With_ThreeChildren_And_MinSizeThatTakesUpMoreSpace()
        {
            var child0 = Substitute.For<ILayoutable>();
            child0.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child1 = Substitute.For<ILayoutable>();
            child1.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var child2 = Substitute.For<ILayoutable>();
            child2.RectRequest.Returns(UIRect.With(0, 0, 10, 10));
            
            var adjacentLayout = Layouts.AdjacentLayout.Of(OrientationOptions.Horizontal, 5);
            var rectRequest = adjacentLayout.CalculateValidRectRequest(new[] {child0, child1, child2}, UISize.Of(300));

            Assert.That(rectRequest, Is.EqualTo(UIRect.With(0, 0, 300, 300)));
        }
    }
}