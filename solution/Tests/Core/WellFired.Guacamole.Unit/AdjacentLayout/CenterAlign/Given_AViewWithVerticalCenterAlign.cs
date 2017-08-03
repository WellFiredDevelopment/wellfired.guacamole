using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.AdjacentLayout.CenterAlign
{
    [TestFixture]
    public class Given_AViewWithVerticalCenterAlign
    {
        [Test]
        public void When_Layout_InSquareParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Expand);
            child.VerticalLayout.Returns(LayoutOptions.Center);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(80, 80));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 100));

            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 40, 80)));
            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(0, 20, 40, 40)));
        }
        
        [Test]
        public void When_Layout_InPortraitParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Expand);
            child.VerticalLayout.Returns(LayoutOptions.Center);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(80, 100));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 100));

            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 40, 100)));
            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(0, 30, 40, 40)));
        }
        
        [Test]
        public void When_Layout_InLandscapeParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Expand);
            child.VerticalLayout.Returns(LayoutOptions.Center);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(100, 80));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 100));

            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 40, 80)));
            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(0, 20, 40, 40)));
        }
    }
}