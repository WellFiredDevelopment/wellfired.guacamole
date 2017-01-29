using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.CenterAlign
{
    [TestFixture]
    public class Given_AViewWithVerticalCenterAlign
    {
        [Test]
        public void When_Layout_Then_LayoutIsCorrect()
        {
            var child = Substitute.For<IView>();
            child.VerticalLayout.Returns(LayoutOptions.Center);
            child.MinSize.Returns(UISize.Of(40, 40));
            child.Content.Returns(default(IView));

            var parentView = Substitute.For<IView>();
            parentView.MinSize.Returns(UISize.Of(80, 80));
            parentView.Content.Returns(child);

            ViewSizingExtensions.DosizingAndLayout(parentView, UIRect.With(0, 0, 80, 80));

            Assert.That(child.RectRequest, Is.EqualTo(UIRect.With(00, 20, 40, 40)));
        }
    }
}