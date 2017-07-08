using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Layout.CenterAndExpandChildren
{
    [TestFixture]
    public class Given_AnVerticalAdjacentLayout_WithAllChildrenCenteredVertically
    {
        [Test]
        public void With_OneChildWithVerticalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHEVC();
            LayoutFactory.VerticalLayoutWith(child0);
            
            Assert.That(child0.X, Is.EqualTo(0));
            Assert.That(child0.Y, Is.EqualTo(90));
        }
        
        [Test]
        public void With_TwoChildrenWithVerticalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHEVC();
            var child1 = LayoutFactory.ChildWithHEVC();
            LayoutFactory.VerticalLayoutWith(child0, child1);

            // Vertical layout heigt is 200, width is 60.
            // Children with vertical center should split the size of their parent and center.
            // 200 split is 100.
            // center of 100 is 50, hence..
            
            Assert.That(child0.X, Is.EqualTo(0));
            Assert.That(child0.Y, Is.EqualTo(40));
            Assert.That(child1.X, Is.EqualTo(0));
            Assert.That(child1.Y, Is.EqualTo(140));
        }
        
        [Test]
        public void With_ThreeChildrenWithVerticalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHEVC();
            var child1 = LayoutFactory.ChildWithHEVC();
            var child2 = LayoutFactory.ChildWithHEVC();
            LayoutFactory.VerticalLayoutWith(child0, child1, child2);

            // Vertical layout heigt is 200, width is 60.
            // Children with vertical center should split the size of their parent and center.
            // 200 split is 66.666667.
            // first cell center = 33.33
            // second cell center = 100
            // third cell center = 166.66
            
            Assert.That(child0.X, Is.EqualTo(0));
            Assert.That(child0.Y, Is.EqualTo(23));
            Assert.That(child1.X, Is.EqualTo(0));
            Assert.That(child1.Y, Is.EqualTo(89));
            Assert.That(child2.X, Is.EqualTo(0));
            Assert.That(child2.Y, Is.EqualTo(155));
        }
        
        [Test]
        public void With_FourChildrenWithVerticalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHEVC();
            var child1 = LayoutFactory.ChildWithHEVC();
            var child2 = LayoutFactory.ChildWithHEVC();
            var child3 = LayoutFactory.ChildWithHEVC();
            LayoutFactory.VerticalLayoutWith(child0, child1, child2, child3);

            // Vertical layout heigt is 200, width is 60.
            // Children with vertical center should split the size of their parent and center.
            // 200 split is 50.
            // center of 100 is 50, hence..
            
            Assert.That(child0.X, Is.EqualTo(0));
            Assert.That(child0.Y, Is.EqualTo(15));
            Assert.That(child1.X, Is.EqualTo(0));
            Assert.That(child1.Y, Is.EqualTo(65));
            Assert.That(child2.X, Is.EqualTo(0));
            Assert.That(child2.Y, Is.EqualTo(115));
            Assert.That(child3.X, Is.EqualTo(0));
            Assert.That(child3.Y, Is.EqualTo(165));
        }
    }
}