using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
    public partial struct UIColor
    {
        [PublicAPI]
        public static UIColor LightGoldenrodYellow
        {
            get { return FromRGB(250, 250, 210); }
        }

        [PublicAPI]
        public static UIColor LightYellow
        {
            get { return FromRGB(255, 255, 224); }
        }

        [PublicAPI]
        public static UIColor Yellow
        {
            get { return FromRGB(255, 255, 0); }
        }

        [PublicAPI]
        public static UIColor Gold
        {
            get { return FromRGB(255, 215, 0); }
        }

        [PublicAPI]
        public static UIColor LightGoldenrod
        {
            get { return FromRGB(238, 221, 130); }
        }

        [PublicAPI]
        public static UIColor Goldenrod
        {
            get { return FromRGB(218, 165, 32); }
        }

        [PublicAPI]
        public static UIColor DarkGoldenrod
        {
            get { return FromRGB(184, 134, 11); }
        }
    }
}