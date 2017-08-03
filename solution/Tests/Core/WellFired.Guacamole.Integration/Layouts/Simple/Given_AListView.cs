using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Simple
{
    [TestFixture]
    public class Given_AListView_With_Content
    {
        [Test]
        public void When_TheListViewIsLayout_Then_LayoutIsSuccessful()
        {
            var listView = new Views.ListView { ItemSource = ItemSource.From("One", "Two", "Three") };
            ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(100, 30));
            Assert.That(listView.RectRequest, Is.EqualTo(UIRect.With(100, 30)));
        }
    }
}