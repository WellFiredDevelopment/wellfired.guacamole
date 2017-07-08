using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Layout.CenterAndExpandChildren
{
    [TestFixture]
    public class Given_AnVerticalAdjacentLayout_WithAllChildrenCenteredHorizontally
    {
        [Test]
        public void With_OneChildWithHorizontalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHCVE();
            LayoutFactory.VerticalLayoutWith(child0);
            
            Assert.That(child0.X, Is.EqualTo(20));
            Assert.That(child0.Y, Is.EqualTo(0));
        }
        
        [Test]
        public void With_TwoChildrenWithHorizontalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHCVE();
            var child1 = LayoutFactory.ChildWithHCVE();
            LayoutFactory.VerticalLayoutWith(child0, child1);
            
            Assert.That(child0.X, Is.EqualTo(20));
            Assert.That(child0.Y, Is.EqualTo(0));
            Assert.That(child1.X, Is.EqualTo(20));
            Assert.That(child1.Y, Is.EqualTo(20));
        }
        
        [Test]
        public void With_FourChildrenWithHorizontalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHCVE();
            var child1 = LayoutFactory.ChildWithHCVE();
            var child2 = LayoutFactory.ChildWithHCVE();
            var child3 = LayoutFactory.ChildWithHCVE();
            LayoutFactory.VerticalLayoutWith(child0, child1, child2, child3);
            
            Assert.That(child0.X, Is.EqualTo(20));
            Assert.That(child0.Y, Is.EqualTo(0));
            Assert.That(child1.X, Is.EqualTo(20));
            Assert.That(child1.Y, Is.EqualTo(20));
            Assert.That(child2.X, Is.EqualTo(20));
            Assert.That(child2.Y, Is.EqualTo(40));
            Assert.That(child3.X, Is.EqualTo(20));
            Assert.That(child3.Y, Is.EqualTo(60));
        }
    }
}