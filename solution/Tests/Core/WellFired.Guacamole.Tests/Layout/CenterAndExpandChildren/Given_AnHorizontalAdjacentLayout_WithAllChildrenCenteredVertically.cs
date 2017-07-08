using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Layout.CenterAndExpandChildren
{
    [TestFixture]
    public class Given_AnHorizontalAdjacentLayout_WithAllChildrenCenteredVertically
    {
        [Test]
        // For a single child with vertical center align, the Y should be centered, the X should be origin, since we're set to expand.
        // Vertical center would be height / 2 - size of element.
        public void With_OneChildWithVerticalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHEVC();
            LayoutFactory.HorizontalLayoutWith(child0);
            
            Assert.That(child0.X, Is.EqualTo(0));
            Assert.That(child0.Y, Is.EqualTo(20));
        }

        [Test]
        // For a two children with vertical center align, the Y should be centered, the X should be increment by the 
        // size of the children, since we're set to expand.
        // Vertical center would be height / 2 - size of element.
        public void With_TwoChildrenWithVerticalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHEVC();
            var child1 = LayoutFactory.ChildWithHEVC();
            LayoutFactory.HorizontalLayoutWith(child0, child1);
            
            Assert.That(child0.X, Is.EqualTo(0));
            Assert.That(child0.Y, Is.EqualTo(20));
            Assert.That(child1.X, Is.EqualTo(20));
            Assert.That(child1.Y, Is.EqualTo(20));
        }

        [Test]
        // For a two children with vertical center align, the Y should be centered, the X should be increment by the 
        // size of the children, since we're set to expand.
        // Vertical center would be height / 2 - size of element.
        public void With_ThreeChildrenWithVerticalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHEVC();
            var child1 = LayoutFactory.ChildWithHEVC();
            var child2 = LayoutFactory.ChildWithHEVC();
            LayoutFactory.HorizontalLayoutWith(child0, child1, child2);
            
            Assert.That(child0.X, Is.EqualTo(0));
            Assert.That(child0.Y, Is.EqualTo(20));
            Assert.That(child1.X, Is.EqualTo(20));
            Assert.That(child1.Y, Is.EqualTo(20));
            Assert.That(child2.X, Is.EqualTo(40));
            Assert.That(child2.Y, Is.EqualTo(20));
        }

        [Test]
        // For a two children with vertical center align, the Y should be centered, the X should be increment by the 
        // size of the children, since we're set to expand.
        // Vertical center would be height / 2 - size of element.
        public void With_FourChildrenWithVerticalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHEVC();
            var child1 = LayoutFactory.ChildWithHEVC();
            var child2 = LayoutFactory.ChildWithHEVC();
            var child3 = LayoutFactory.ChildWithHEVC();
            LayoutFactory.HorizontalLayoutWith(child0, child1, child2, child3);
            
            Assert.That(child0.X, Is.EqualTo(0));
            Assert.That(child0.Y, Is.EqualTo(20));
            Assert.That(child1.X, Is.EqualTo(20));
            Assert.That(child1.Y, Is.EqualTo(20));
            Assert.That(child2.X, Is.EqualTo(40));
            Assert.That(child2.Y, Is.EqualTo(20));
            Assert.That(child3.X, Is.EqualTo(60));
            Assert.That(child3.Y, Is.EqualTo(20));
        }
    }
}