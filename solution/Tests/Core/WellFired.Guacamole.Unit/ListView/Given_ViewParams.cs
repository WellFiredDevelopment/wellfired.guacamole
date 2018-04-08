using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.ListView
{
    [TestFixture]
    public class GivenViewParams
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
        
        [Test]
        public void When_AvailableSpace_Bigger_Than_ListSize_Then_ScrollOffset_Set_To_Zero()
        {
            var listView = new Views.ListView
            {
                EntrySize = 50,
                Spacing = 5,
                ItemSource = ItemSource.From("Sausage", "Guacamole", "Saucisson")
            };

            listView.ScrollOffset = 10;
            
            ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(0, 0, 300, 30));
            
            Assert.That(listView.AvailableSpace, Is.EqualTo(30));
            Assert.That(listView.AvailableSpace, Is.EqualTo(30));
            Assert.That(listView.ScrollOffset, Is.EqualTo(10));
            
            ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(0, 0, 300, 250));
            
            Assert.That(listView.AvailableSpace, Is.EqualTo(250));
            Assert.That(listView.CanScroll, Is.False);
            Assert.That(listView.ScrollOffset, Is.EqualTo(0));
        }
        
        [Test]
        public void When_ScrollShouldBeShown_Then_List_Items_Are_Correctly_Sized()
        {
            //Vertical orientation
            var verticalListView = Substitute.For<IListView>();
            verticalListView.ShouldShowScrollBar.Returns(true);
            verticalListView.ScrollBarSize.Returns(10);
            
            //spacing should not have any impact. We set it here to test undesired coupling.
            verticalListView.Spacing.Returns(5);
            
            verticalListView.Orientation.Returns(OrientationOptions.Vertical);

            verticalListView.RectRequest = UIRect.With(0, 0, 180, 200);

            var view = new Views.View();

            ListViewHelper.ConstrainToCell(verticalListView, view);
            
            Assert.That(view.RectRequest.Width, Is.EqualTo(180 - 10));
            
            //Horizontal orientation
            var horizontalListView = Substitute.For<IListView>();
            horizontalListView.ShouldShowScrollBar.Returns(true);
            horizontalListView.ScrollBarSize.Returns(10);
            
            //spacing should not have any impact. We set it here to test undesired coupling.
            horizontalListView.Spacing.Returns(5);
            
            horizontalListView.Orientation.Returns(OrientationOptions.Horizontal);

            horizontalListView.RectRequest = UIRect.With(0, 0, 180, 200);

            ListViewHelper.ConstrainToCell(horizontalListView, view);
            
            Assert.That(view.RectRequest.Height, Is.EqualTo(200 - 10));
        }
    }
}