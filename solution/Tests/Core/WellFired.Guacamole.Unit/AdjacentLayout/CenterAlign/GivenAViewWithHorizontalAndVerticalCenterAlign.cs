using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.AdjacentLayout.CenterAlign
{
    [TestFixture]
    public class GivenAViewWithHorizontalAndVerticalCenterAlign
    {
        [Test]
        public void When_Layout_InSquareParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Center);
            child.VerticalLayout.Returns(LayoutOptions.Center);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(80, 80));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 80));

            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 80, 80)));
            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(20, 20, 40, 40)));
        }
        
        [Test]
        public void When_Layout_InLanscapeParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Center);
            child.VerticalLayout.Returns(LayoutOptions.Center);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(100, 80));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 80));

            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 80)));
            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(30, 20, 40, 40)));
        }
        
        [Test]
        public void When_Layout_InPortraitParentView_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.HorizontalLayout.Returns(LayoutOptions.Center);
            child.VerticalLayout.Returns(LayoutOptions.Center);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(80, 100));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 80, 80));

            Assert.That(child.ContentRectRequest, Is.EqualTo(UIRect.With(20, 30, 40, 40)));
            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(0, 0, 80, 100)));
        }
    }
}