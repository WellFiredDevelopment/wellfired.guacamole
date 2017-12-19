using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Drawing.Extensions
{
    public static class OutlineMaskExtensions
    {
        public static bool Is(this OutlineMask source, OutlineMask cornerMask)
        {
            return (source & cornerMask) != 0;
        }
    }
}