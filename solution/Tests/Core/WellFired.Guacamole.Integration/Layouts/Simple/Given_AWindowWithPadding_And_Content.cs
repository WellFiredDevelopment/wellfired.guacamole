using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Simple
{
    [TestFixture]
    public class GivenAWindowWithPaddingAndContent
    {
        [Test]
        public void When_LayoutOccurs_Then_LayoutIsSuccessful()
        {
            var content = new LabelView
            {
                Text = "Sausages",
                MinSize = UISize.Of(80, 13)
            };

            var logger = NSubstitute.Substitute.For<Diagnostics.ILogger>();
            var window = new Window (logger, default(IPlatformProvider)) {
                Padding = UIPadding.Of(10),
                Content = content
            };

            window.Layout(UIRect.With(0, 0, 100, 50));
            Assert.That(content.RectRequest, Is.EqualTo(UIRect.With(10, 10, 80, 13)));
        }
    }
}