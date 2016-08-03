using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
    public partial struct UIColor
    {
        [PublicAPI]
        public static UIColor DarkSalmon
        {
            get { return FromRGB(233, 150, 122); }
        }

        [PublicAPI]
        public static UIColor Salmon
        {
            get { return FromRGB(250, 128, 114); }
        }

        [PublicAPI]
        public static UIColor LightSalmon
        {
            get { return FromRGB(255, 160, 122); }
        }

        [PublicAPI]
        public static UIColor Orange
        {
            get { return FromRGB(255, 165, 0); }
        }

        [PublicAPI]
        public static UIColor DarkOrange
        {
            get { return FromRGB(255, 140, 0); }
        }

        [PublicAPI]
        public static UIColor Coral
        {
            get { return FromRGB(255, 127, 80); }
        }

        [PublicAPI]
        public static UIColor LightCoral
        {
            get { return FromRGB(240, 128, 128); }
        }

        [PublicAPI]
        public static UIColor Tomato
        {
            get { return FromRGB(255, 99, 71); }
        }

        [PublicAPI]
        public static UIColor OrangeRed
        {
            get { return FromRGB(255, 69, 0); }
        }

        [PublicAPI]
        public static UIColor Red
        {
            get { return FromRGB(255, 0, 0); }
        }
    }
}