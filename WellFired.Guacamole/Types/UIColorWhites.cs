using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
    public partial struct UIColor
    {
        [PublicAPI]
        public static UIColor Snow
        {
            get { return FromRGB(255, 250, 250); }
        }

        [PublicAPI]
        public static UIColor GhostWhite
        {
            get { return FromRGB(248, 248, 255); }
        }

        [PublicAPI]
        public static UIColor WhiteSmoke
        {
            get { return FromRGB(245, 245, 245); }
        }

        [PublicAPI]
        public static UIColor FloralWhite
        {
            get { return FromRGB(255, 250, 240); }
        }

        [PublicAPI]
        public static UIColor White
        {
            get { return FromRGB(255, 255, 255); }
        }

        [PublicAPI]
        public static UIColor Clear
        {
            get { return FromRGBA(255, 255, 255, 0); }
        }
    }
}