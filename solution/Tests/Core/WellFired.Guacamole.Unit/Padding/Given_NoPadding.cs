using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Padding
{
    [TestFixture]
    public class GivenNoPadding
    {
        [Test]
        public void When_AdjustingAViewWithoutPadding_Then_ReturnedRectIsCorrect()
        {
            var size = UISize.Of(100, 100);
            var padding = UIPadding.Of(0);
            
            var returnedRect = ViewPaddingCalculation.AdjustRectRequestForPadding(padding, size);
            
            Assert.That(returnedRect, Is.EqualTo(UISize.Of(100, 100)));
        }
    }
}