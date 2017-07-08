using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Layout.CenterAndExpandChildren
{
    [TestFixture]
    public class Given_AnHorizontalAdjacentLayout_WithAllChildrenCenteredHorizontally
    {
        [Test]
        // For a single child with horizontal center align, the Y should be the origin, the X should be centered,
        // Horizontal center would be width / 2 - size of element.
        public void With_OneChildWithHorizontalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHCVE();
            LayoutFactory.HorizontalLayoutWith(child0);
            
            Assert.That(child0.X, Is.EqualTo(90));
            Assert.That(child0.Y, Is.EqualTo(0));
        }

        [Test]
        // For two children with horizontal center align, the parent element should have it's width divided by two. 
        // This width should then be used to center each children, in each virtual cell.
        public void With_TwoChildrenWithHorizontalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHCVE();
            var child1 = LayoutFactory.ChildWithHCVE();
            LayoutFactory.HorizontalLayoutWith(child0, child1);
            
            Assert.That(child0.X, Is.EqualTo(40));
            Assert.That(child0.Y, Is.EqualTo(0));
            Assert.That(child1.X, Is.EqualTo(140));
            Assert.That(child1.Y, Is.EqualTo(0));
        }

        [Test]
        // For three children with horizontal center align, the parent element should have it's width divided by three. 
        // This width should then be used to center each children, in each virtual cell.
        public void With_ThreeChildrenWithHorizontalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHCVE();
            var child1 = LayoutFactory.ChildWithHCVE();
            var child2 = LayoutFactory.ChildWithHCVE();
            LayoutFactory.HorizontalLayoutWith(child0, child1, child2);

            // 200 total
            // 66.667 in each cell
            // first cell center = 33.33
            // second cell center = 100
            // third cell center = 166.66
            
            Assert.That(child0.X, Is.EqualTo(23));
            Assert.That(child0.Y, Is.EqualTo(0));
            Assert.That(child1.X, Is.EqualTo(89));
            Assert.That(child1.Y, Is.EqualTo(0));
            Assert.That(child2.X, Is.EqualTo(155));
            Assert.That(child2.Y, Is.EqualTo(0));
        }

        [Test]
        // For four children with horizontal center align, the parent element should have it's width divided by four. 
        // This width should then be used to center each children, in each virtual cell.
        public void With_FourChildrenWithHorizontalCentreAlign_When_Layout_Then_LayoutIsCorrect()
        {
            var child0 = LayoutFactory.ChildWithHCVE();
            var child1 = LayoutFactory.ChildWithHCVE();
            var child2 = LayoutFactory.ChildWithHCVE();
            var child3 = LayoutFactory.ChildWithHCVE();
            LayoutFactory.HorizontalLayoutWith(child0, child1, child2, child3);

            // 200 total
            // 50 in each cell
            // first cell center = 25
            // second cell center = 75
            // third cell center = 125
            // fourth cell center = 175
            
            Assert.That(child0.X, Is.EqualTo(15));
            Assert.That(child0.Y, Is.EqualTo(0));
            Assert.That(child1.X, Is.EqualTo(65));
            Assert.That(child1.Y, Is.EqualTo(0));
            Assert.That(child2.X, Is.EqualTo(115));
            Assert.That(child2.Y, Is.EqualTo(0));
            Assert.That(child3.X, Is.EqualTo(165));
            Assert.That(child3.Y, Is.EqualTo(0));
        }
    }
}