using WellFired.Guacamole.Data;
using WellFired.Guacamole.Drawing;

namespace WellFired.Guacamole.Extensions
{
    public static class ColorExtensions
    {
        public static ByteColor ToByteColor(this UIColor color)
        {
            return new ByteColor(color.R.AsByte(), color.G.AsByte(), color.B.AsByte(), color.A.AsByte());
        }
    }
}