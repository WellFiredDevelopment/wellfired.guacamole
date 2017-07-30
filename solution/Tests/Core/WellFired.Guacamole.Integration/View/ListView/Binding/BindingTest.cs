using System.Linq;
using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.View.ListView.Binding
{
    [TestFixture]
    public class BindingTests
    {
        [Test]
        public void WithOneView_OnBindingContextSetAfterConstruction_Then_BindingContextIsSetIsCorrect()
        {
            var context = new ContextObject();
            var view = new Views.ListView {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                Orientation = OrientationOptions.Horizontal,
                EntrySize = 50,
                Spacing = 5,
                ItemSource = ItemSource.From("One")
            };;
            
            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 300, 300));
            
            view.BindingContext = context;
            
            Assert.That(view.BindingContext, Is.EqualTo(context));
            Assert.That(((Views.View)view.Children.First()).BindingContext, Is.Not.EqualTo(context));
        }
    }
}