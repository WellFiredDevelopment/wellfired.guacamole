using NUnit.Framework;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Unit.Drawing.ImageData
{
    [TestFixture]
    public class Given_AnImageParameters
    {
        [Test]
        public void When_AttemptingToBuildAnImageWith4RoundedCorners_Then_BuiltSuccessfully()
        {
            Assert.That(() => Image.ImageData.BuildRounded(128, 128, UIColor.Aquamarine, UIColor.Beige, 0, 1, CornerMask.All, OutlineMask.All), Throws.Nothing);
        }

        [Test]
        public void When_AttemptingToBuildAnSquareImage_Then_BuiltSuccessfully()
        {
            Assert.That(() => Image.ImageData.BuildRect(128, 128, UIColor.Aquamarine, UIColor.Beige, 1, OutlineMask.All), Throws.Nothing);
        }
        
        [Test]
        public void When_AttemptingToBuildAnEllipse_Then_BuiltSuccessfully()
        {
            Assert.That(() => Image.ImageData.BuildEllipse(128, 128, UIColor.Aquamarine, UIColor.Beige, 1), Throws.Nothing);
        }
    }
}