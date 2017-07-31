using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.ListView.Vertical
{
    [TestFixture]
    public class Given_AListView
    {
        [Test]
        public void With_AnItemSource_When_Layout_Then_ChildrenAreCorrect()
        {
            var listView = new Views.ListView
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                Orientation = OrientationOptions.Vertical,
                EntrySize = 20,
                ItemSource = ItemSource.From("One", "Two", "Three", "Four")
            };

            ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(500, 1000));

            Assert.That(listView.Children.Count, Is.EqualTo(4));

            Assert.That(listView.Children[0].RectRequest, Is.EqualTo(UIRect.With(0, 0, 0, 20)));
            Assert.That(listView.Children[1].RectRequest, Is.EqualTo(UIRect.With(0, 20, 0, 20)));
            Assert.That(listView.Children[2].RectRequest, Is.EqualTo(UIRect.With(0, 40, 0, 20)));
            Assert.That(listView.Children[3].RectRequest, Is.EqualTo(UIRect.With(0, 60, 0, 20)));
        }
    }
}