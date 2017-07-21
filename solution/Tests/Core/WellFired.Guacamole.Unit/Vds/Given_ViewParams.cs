using NUnit.Framework;
using WellFired.Guacamole.Views;

// ReSharper disable ArgumentsStyleLiteral

namespace WellFired.Guacamole.Unit.Vds
{
    [TestFixture]
    public class Given_ViewParams
    {
        [Test]
        public void With_NoScrollShowingTwoElements_ExpectedResultIsCorrect()
        {
            var expectedVds = new[] { 0, 1 };
            var vds = VdsCalculator.CalculateVisualDataSet(
                virtualScrollPosition: 0, 
                visibleControlSize: 100, 
                estimatedElementSize: 50, 
                estimatedContentSize: 200, 
                spacing: 0);
            
            Assert.That(vds, Is.EquivalentTo(expectedVds));
        }
        
        [Test]
        public void With_ScrollShowingTwoElements_And_OnTheStartBoundaryOfAThird_ExpectedResultIsCorrect()
        {
            var expectedVds = new[] { 0, 1, 2 };
            var vds = VdsCalculator.CalculateVisualDataSet(
                virtualScrollPosition: 1, 
                visibleControlSize: 100, 
                estimatedElementSize: 50, 
                estimatedContentSize: 200, 
                spacing: 0);
            
            Assert.That(vds, Is.EquivalentTo(expectedVds));
        }
        
        [Test]
        public void With_ScrollShowingTwoElements_And_ScrolledToTheSecond_ExpectedResultIsCorrect()
        {
            var expectedVds = new[] { 1, 2 };
            var vds = VdsCalculator.CalculateVisualDataSet(
                virtualScrollPosition: 50, 
                visibleControlSize: 100, 
                estimatedElementSize: 50, 
                estimatedContentSize: 200, 
                spacing: 0);
            
            Assert.That(vds, Is.EquivalentTo(expectedVds));
        }
        
        [Test]
        public void With_OneHundredEntries_And_ScrolledToTheEnd_ExpectedResultIsCorrect()
        {
            var expectedVds = new[] { 98, 99 };
            var vds = VdsCalculator.CalculateVisualDataSet(
                virtualScrollPosition: 980,
                visibleControlSize: 20, 
                estimatedElementSize: 10, 
                estimatedContentSize: 1000, 
                spacing: 0);
            
            Assert.That(vds, Is.EquivalentTo(expectedVds));
        }
        
        [Test]
        public void With_OneThousandEntries_And_ScrolledToTheEnd_ExpectedResultIsCorrect()
        {
            var expectedVds = new[] { 998, 999 };
            var vds = VdsCalculator.CalculateVisualDataSet(
                virtualScrollPosition: 9980,
                visibleControlSize: 20, 
                estimatedElementSize: 10, 
                estimatedContentSize: 10000, 
                spacing: 0);
            
            Assert.That(vds, Is.EquivalentTo(expectedVds));
        }
        
        [Test]
        public void With_TenThousandEntries_And_ScrolledToTheEnd_ExpectedResultIsCorrect()
        {
            var expectedVds = new[] { 9998, 9999 };
            var vds = VdsCalculator.CalculateVisualDataSet(
                virtualScrollPosition: 99980,
                visibleControlSize: 20, 
                estimatedElementSize: 10, 
                estimatedContentSize: 100000, 
                spacing: 0);
            
            Assert.That(vds, Is.EquivalentTo(expectedVds));
        }
        
        [Test]
        public void With_HundredThousandEntries_And_ScrolledToTheEnd_ExpectedResultIsCorrect()
        {
            var expectedVds = new[] { 99998, 99999 };
            var vds = VdsCalculator.CalculateVisualDataSet(
                virtualScrollPosition: 999980,
                visibleControlSize: 20, 
                estimatedElementSize: 10, 
                estimatedContentSize: 1000000, 
                spacing: 0);
            
            Assert.That(vds, Is.EquivalentTo(expectedVds));
        }
        
        [Test]
        public void With_MillionEntries_And_ScrolledToTheEnd_ExpectedResultIsCorrect()
        {
            var expectedVds = new[] { 999998, 999999 };
            var vds = VdsCalculator.CalculateVisualDataSet(
                virtualScrollPosition: 9999980,
                visibleControlSize: 20, 
                estimatedElementSize: 10, 
                estimatedContentSize: 10000000, 
                spacing: 0);
            
            Assert.That(vds, Is.EquivalentTo(expectedVds));
        }
    }
}