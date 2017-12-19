using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.AdjacentLayout.CenterAlign
{
    [TestFixture]
    public class GivenAViewWithHorizontalCenterAlign
    {
        [Test]
        public void When_Layout_InSquareParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Center);
            child.VerticalLayout.Returns(LayoutOptions.Expand);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(80, 80));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 80));
            
            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 80, 40)));
            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(20, 00, 40, 40)));
        }
        
        [Test]
        public void When_Layout_InPortraitParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Center);
            child.VerticalLayout.Returns(LayoutOptions.Expand);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(80, 100));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 80));

            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 80, 40)));
            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(20, 0, 40, 40)));
        }
        
        [Test]
        public void When_Layout_InLandscapeParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Center);
            child.VerticalLayout.Returns(LayoutOptions.Expand);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(100, 80));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 80));

            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 80, 40)));
            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(20, 00, 40, 40)));
        }

        [Test]
        public void And_ContentWithContent_Then_LayoutIsCorrect()
        {
            var child1 = Substitute.For<IView>();
            child1.MinSize.Returns(UISize.Of(10, 10));
            child1.Content.Returns(default(IView));

            var child0 = Substitute.For<IView>();
            child0.HorizontalLayout.Returns(LayoutOptions.Center);
            child0.MinSize.Returns(UISize.Of(40, 40));
            child0.Content.Returns(child1);

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(80, 80));
            parentView.Content.Returns(child0);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 80));

            Assert.That(child0.RectRequest, Is.EqualTo(UIRect.With(0, 0, 80, 40)));
            Assert.That(child0.ContentRectRequest, Is.EqualTo(UIRect.With(20, 0, 40, 40)));
            
            Assert.That(child1.RectRequest, Is.EqualTo(UIRect.With(0, 0, 10, 10)));
            Assert.That(child1.ContentRectRequest, Is.EqualTo(UIRect.With(0, 0, 10, 10)));
        }
    }
}