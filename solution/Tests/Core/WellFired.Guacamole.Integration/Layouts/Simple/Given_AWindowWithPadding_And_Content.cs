using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Simple
{
    [TestFixture]
    public class Given_AWindowWithPadding_And_Content
    {
        [Test]
        public void When_LayoutOccurs_Then_LayoutIsSuccessful()
        {
            var content = new Label
            {
                Text = "Sausages",
                MinSize = UISize.Of(80, 13)
            };

            var window = new Window
            {
                Padding = UIPadding.Of(10),
                Content = content
            };

            window.Layout(UIRect.With(0, 0, 100, 50));
            Assert.That(content.RectRequest, Is.EqualTo(UIRect.With(10, 10, 80, 13)));
        }
    }
}