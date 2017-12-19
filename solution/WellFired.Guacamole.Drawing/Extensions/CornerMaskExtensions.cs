using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Drawing.Extensions
{
    public static class CornerMaskExtensions
    {
        public static bool Is(this CornerMask source, CornerMask cornerMask)
        {
            return (source & cornerMask) != 0;
        }
    }
}