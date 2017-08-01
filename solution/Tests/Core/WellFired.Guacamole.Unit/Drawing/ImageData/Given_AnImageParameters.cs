using NUnit.Framework;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Unit.Drawing.ImageData
{
    [TestFixture]
    public class Given_AnImageParameters
    {
        [Test]
        public void When_AttemptingToBuildAnImageWith4RoundedCorners_Then_BuiltSuccessfully()
        {
            Assert.That(() => Image.ImageData.BuildRounded(128, 128, UIColor.Aquamarine, UIColor.Beige, 1, CornerMask.All), Throws.Nothing);
        }
        
        [Test]
        public void When_AttemptingToBuildAnSquareImage_Then_BuiltSuccessfully()
        {
            Assert.That(() => Image.ImageData.BuildSquare(128, 128, UIColor.Aquamarine, UIColor.Beige), Throws.Nothing);
        }
    }
}