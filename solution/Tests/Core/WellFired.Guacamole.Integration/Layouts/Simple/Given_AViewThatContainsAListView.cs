using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Simple
{
    [TestFixture]
    public class Given_AViewThatContainsAListView
    {
        [Test]
        public void When_TheListViewIsLayout_Then_LayoutIsSuccessful()
        {
            var listView = new ListView {
                EntrySize = 20,
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
        }
    }
}