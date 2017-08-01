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
            var maxScroll = ListViewHelper.MaxScrollFor(10, 0, 100, 10);
            Assert.That(maxScroll, Is.EqualTo(0));
        }
        
        [Test]
        public void With_TwentyEntries_That_FitExactly_ThenMaxScrollIsCorrect()
        {
            var maxScroll = ListViewHelper.MaxScrollFor(20, 0, 200, 10);
            Assert.That(maxScroll, Is.EqualTo(0));
        }
        
        [Test]
        public void With_FewerEntriesThanSpaceAvailable_ThenMaxScrollIsCorrect()
        {
            var maxScroll = ListViewHelper.MaxScrollFor(2, 0, 20, 10);
            Assert.That(maxScroll, Is.EqualTo(0));
        }
        
        [Test]
        public void With_MoreEntriesThanSpaceAvailable_ThenMaxScrollIsCorrect()
        {
            // If we have 2 visible entries and each entry is size 2, but a total content size of 20, then we have 10 entries in our data set.
            var maxScroll = ListViewHelper.MaxScrollFor(2, 0, 20, 2);
            // If we can fit two entries on screen at once and 10 total entries then we can scroll a maximum of 8 entries deep. 8 * 2 is 16
            Assert.That(maxScroll, Is.EqualTo(16));
        }
    }
}