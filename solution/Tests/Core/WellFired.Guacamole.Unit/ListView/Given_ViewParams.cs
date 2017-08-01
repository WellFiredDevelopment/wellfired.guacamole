using NUnit.Framework;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.ListView
{
    [TestFixture]
    public class Given_ViewParams
    {
        [Test]
        public void With_TenEntries_That_FitExactly_ThenMaxScrollIsCorrect()
        {
            var maxScroll = ListViewHelper.MaxScrollFor(10, 10);
            Assert.That(maxScroll, Is.EqualTo(0));
        }
        
        [Test]
        public void With_TwentyEntries_That_FitExactly_ThenMaxScrollIsCorrect()
        {
            var maxScroll = ListViewHelper.MaxScrollFor(20, 20);
            Assert.That(maxScroll, Is.EqualTo(0));
        }
        
        [Test]
        public void With_FewerEntriesThanSpaceAvailable_ThenMaxScrollIsCorrect()
        {
            var maxScroll = ListViewHelper.MaxScrollFor(30, 30);
            Assert.That(maxScroll, Is.EqualTo(0));
        }
        
        [Test]
        public void With_MoreEntriesThanSpaceAvailable_ThenMaxScrollIsCorrect()
        {
            var maxScroll = ListViewHelper.MaxScrollFor(4, 20);
            Assert.That(maxScroll, Is.EqualTo(16));
        }
    }
}