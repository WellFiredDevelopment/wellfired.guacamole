using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.ListView.Horizontal
{
    [TestFixture]
    public class GivenAListView
    {
        [Test]
        public void With_Expand_And_AnItemSource_When_Layout_Then_ChildrenAreCorrect()
        {
            var listView = new Views.ListView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Expand,
                Orientation = OrientationOptions.Horizontal,
                EntrySize = 20,
                ItemSource = ItemSource.From("One", "Two", "Three", "Four")
            };
            
            ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(1000, 500));
            
            Assert.That(listView.Children.Count, Is.EqualTo(4));
            
            Assert.That(listView.Children[0].RectRequest, Is.EqualTo(UIRect.With(0, 0, 20, 0)));
            Assert.That(listView.Children[1].RectRequest, Is.EqualTo(UIRect.With(20, 0, 20, 0)));
            Assert.That(listView.Children[2].RectRequest, Is.EqualTo(UIRect.With(40, 0, 20, 0)));
            Assert.That(listView.Children[3].RectRequest, Is.EqualTo(UIRect.With(60, 0, 20, 0)));
        }
    }
}