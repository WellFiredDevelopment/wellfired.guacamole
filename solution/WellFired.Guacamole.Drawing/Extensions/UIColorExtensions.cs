using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Drawing.Extensions
{
    public static class UIColorExtensions
    {
        public static bool IsTheSameForFloodFill(this UIColor replace, UIColor with)
        {
            if (replace.A < 0.3f)
                return true;
            
            return replace == with;
        }

        public static UIColor GetBlend(UIColor replace, UIColor with)
        {
            var alpha = with.A;
            return with * alpha + replace * (1 - alpha);
        }
    }
}