using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Simple
{
    [TestFixture]
    public class Given_AViewWithAListView
    {
        [Test]
        public void When_TheListViewIsLayout_Then_LayoutIsSuccessful()
        {
            var listView = new ListView {
                ItemSource = ItemSource.From("One", "Two", "Three")
            };

            var parentView = new Views.View {
                Padding = UIPadding.Of(10),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Content = listView
            };

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(100, 100));

            Assert.That(parentView.RectRequest, Is.EqualTo(UIRect.With(100, 100)));
            Assert.That(listView.RectRequest, Is.EqualTo(UIRect.With(10, 10, 80, 80)));

            // This is expected to be 0, since it's relative to the parent. In this case, the listView
            Assert.That(listView.Children[0].RectRequest, Is.EqualTo(UIRect.With(0, 00, 50, 20)));
            Assert.That(listView.Children[1].RectRequest, Is.EqualTo(UIRect.With(0, 20, 50, 20)));
            Assert.That(listView.Children[2].RectRequest, Is.EqualTo(UIRect.With(0, 40, 50, 20)));
        }
    }
}