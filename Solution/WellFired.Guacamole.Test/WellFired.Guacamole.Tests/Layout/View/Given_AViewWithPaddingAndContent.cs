using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Tests.Layout.View
{
    public class Given_AViewWithPaddingAndContent
    {
        [Test]
        public void When_TheViewIsLayout_Then_LayoutIsCorrect()
        {
            var content = Substitute.For<IView>();
            content.HorizontalLayout.Returns(LayoutOptions.Fill);
            content.VerticalLayout.Returns(LayoutOptions.Fill);
            content.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.HorizontalLayout.Returns(LayoutOptions.Fill);
            parentView.VerticalLayout.Returns(LayoutOptions.Fill);
            parentView.Padding.Returns(UIPadding.Of(10));
            parentView.Content.Returns(content);

            Assert.That(content, Is.Not.EqualTo(parentView));

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 100, 100));

            Assert.That(parentView.RectRequest.X, Is.EqualTo(0));
            Assert.That(parentView.RectRequest.Y, Is.EqualTo(0));
            Assert.That(parentView.RectRequest.Width, Is.EqualTo(100));
            Assert.That(parentView.RectRequest.Height, Is.EqualTo(100));

            Assert.That(content.RectRequest.X, Is.EqualTo(10));
            Assert.That(content.RectRequest.Y, Is.EqualTo(10));
            Assert.That(content.RectRequest.Width, Is.EqualTo(80));
            Assert.That(content.RectRequest.Height, Is.EqualTo(80));
        }

        [Test]
        public void When_TheContentHasContent_And_TheViewIsLayout_Then_TheLayoutIsCorrect()
        {
            var contentContent = Substitute.For<IView>();
            contentContent.HorizontalLayout.Returns(LayoutOptions.Fill);
            contentContent.VerticalLayout.Returns(LayoutOptions.Fill);
            contentContent.Content.Returns(default(IView));

            var content = Substitute.For<IView>();
            content.HorizontalLayout.Returns(LayoutOptions.Fill);
            content.VerticalLayout.Returns(LayoutOptions.Fill);
            content.Content.Returns(contentContent);

            var parentView = Substitute.For<IView>();
            parentView.HorizontalLayout.Returns(LayoutOptions.Fill);
            parentView.VerticalLayout.Returns(LayoutOptions.Fill);
            parentView.Padding.Returns(UIPadding.Of(10));
            parentView.Content.Returns(content);

            Assert.That(content, Is.Not.EqualTo(parentView));

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 100, 100));

            Assert.That(parentView.RectRequest.X, Is.EqualTo(0));
            Assert.That(parentView.RectRequest.Y, Is.EqualTo(0));
            Assert.That(parentView.RectRequest.Width, Is.EqualTo(100));
            Assert.That(parentView.RectRequest.Height, Is.EqualTo(100));

            Assert.That(content.RectRequest.X, Is.EqualTo(10));
            Assert.That(content.RectRequest.Y, Is.EqualTo(10));
            Assert.That(content.RectRequest.Width, Is.EqualTo(80));
            Assert.That(content.RectRequest.Height, Is.EqualTo(80));

            Assert.That(contentContent.RectRequest.X, Is.EqualTo(0));
            Assert.That(contentContent.RectRequest.Y, Is.EqualTo(0));
            Assert.That(contentContent.RectRequest.Width, Is.EqualTo(80));
            Assert.That(contentContent.RectRequest.Height, Is.EqualTo(80));
        }

        [Test]
        public void When_TheContentHasPadding_And_TheContentHasContent_And_TheViewIsLayout_Then_TheLayoutIsCorrect()
        {
            var contentContent = Substitute.For<IView>();
            contentContent.HorizontalLayout.Returns(LayoutOptions.Fill);
            contentContent.VerticalLayout.Returns(LayoutOptions.Fill);
            contentContent.Content.Returns(default(IView));

            var content = Substitute.For<IView>();
            content.HorizontalLayout.Returns(LayoutOptions.Fill);
            content.VerticalLayout.Returns(LayoutOptions.Fill);
            content.Padding.Returns(UIPadding.Of(10));
            content.Content.Returns(contentContent);

            var parentView = Substitute.For<IView>();
            parentView.HorizontalLayout.Returns(LayoutOptions.Fill);
            parentView.VerticalLayout.Returns(LayoutOptions.Fill);
            parentView.Padding.Returns(UIPadding.Of(10));
            parentView.Content.Returns(content);

            Assert.That(content, Is.Not.EqualTo(parentView));

            ViewSizingExtensions.DoSizingAndLayout(parentView, UIRect.With(0, 0, 100, 100));

            Assert.That(parentView.RectRequest.X, Is.EqualTo(0));
            Assert.That(parentView.RectRequest.Y, Is.EqualTo(0));
            Assert.That(parentView.RectRequest.Width, Is.EqualTo(100));
            Assert.That(parentView.RectRequest.Height, Is.EqualTo(100));

            Assert.That(content.RectRequest.X, Is.EqualTo(10));
            Assert.That(content.RectRequest.Y, Is.EqualTo(10));
            Assert.That(content.RectRequest.Width, Is.EqualTo(80));
            Assert.That(content.RectRequest.Height, Is.EqualTo(80));

            Assert.That(contentContent.RectRequest.X, Is.EqualTo(10));
            Assert.That(contentContent.RectRequest.Y, Is.EqualTo(10));
            Assert.That(contentContent.RectRequest.Width, Is.EqualTo(60));
            Assert.That(contentContent.RectRequest.Height, Is.EqualTo(60));
        }
    }
}