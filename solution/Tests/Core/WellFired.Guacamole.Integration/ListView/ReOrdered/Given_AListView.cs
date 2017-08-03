using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;
using WellFired.Guacamole.Views.BindingContexts;

namespace WellFired.Guacamole.Integration.ListView.ReOrdered
{
    [TestFixture]
    public class Given_AListView
    {
        [Test]
        public void With_AnItemSource_When_Layout_Then_ChildrenAreCorrect()
        {
            var source0 = ItemSource.From("One", "Two", "Three", "Four");
            var source1 = ItemSource.From("Four", "Three", "Two", "One");
            
            var listView = new Views.ListView
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                Orientation = OrientationOptions.Vertical,
                EntrySize = 20,
                ItemSource = source0
            };

            ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(500, 1000));

            Assert.That(((LabelCellBindingContext)((Views.View)listView.Children[0]).BindingContext).CellLabelText, Is.EqualTo("One"));
            Assert.That(((LabelCellBindingContext)((Views.View)listView.Children[1]).BindingContext).CellLabelText, Is.EqualTo("Two"));
            Assert.That(((LabelCellBindingContext)((Views.View)listView.Children[2]).BindingContext).CellLabelText, Is.EqualTo("Three"));
            Assert.That(((LabelCellBindingContext)((Views.View)listView.Children[3]).BindingContext).CellLabelText, Is.EqualTo("Four"));
            
            Assert.That(((Cells.LabelCell)listView.Children[0]).Text, Is.EqualTo("One"));
            Assert.That(((Cells.LabelCell)listView.Children[1]).Text, Is.EqualTo("Two"));
            Assert.That(((Cells.LabelCell)listView.Children[2]).Text, Is.EqualTo("Three"));
            Assert.That(((Cells.LabelCell)listView.Children[3]).Text, Is.EqualTo("Four"));

            listView.ItemSource = source1;
            ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(500, 1000));
            
            Assert.That(((LabelCellBindingContext)((Views.View)listView.Children[0]).BindingContext).CellLabelText, Is.EqualTo("Four"));
            Assert.That(((LabelCellBindingContext)((Views.View)listView.Children[1]).BindingContext).CellLabelText, Is.EqualTo("Three"));
            Assert.That(((LabelCellBindingContext)((Views.View)listView.Children[2]).BindingContext).CellLabelText, Is.EqualTo("Two"));
            Assert.That(((LabelCellBindingContext)((Views.View)listView.Children[3]).BindingContext).CellLabelText, Is.EqualTo("One"));
            
            Assert.That(((Cells.LabelCell)listView.Children[0]).Text, Is.EqualTo("Four"));
            Assert.That(((Cells.LabelCell)listView.Children[1]).Text, Is.EqualTo("Three"));
            Assert.That(((Cells.LabelCell)listView.Children[2]).Text, Is.EqualTo("Two"));
            Assert.That(((Cells.LabelCell)listView.Children[3]).Text, Is.EqualTo("One"));
        }
    }
}