using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
    public partial struct UIColor
    {
        [PublicAPI]
        public static UIColor HotPink
        {
            get { return FromRGB(255, 105, 180); }
        }

        [PublicAPI]
        public static UIColor DeepPink
        {
            get { return FromRGB(255, 20, 147); }
        }

        [PublicAPI]
        public static UIColor Pink
        {
            get { return FromRGB(255, 192, 203); }
        }

        [PublicAPI]
        public static UIColor LightPink
        {
            get { return FromRGB(255, 182, 193); }
        }

        [PublicAPI]
        public static UIColor PaleVioletRed
        {
            get { return FromRGB(219, 112, 147); }
        }

        [PublicAPI]
        public static UIColor Maroon
        {
            get { return FromRGB(176, 48, 96); }
        }

        [PublicAPI]
        public static UIColor MediumVioletRed
        {
            get { return FromRGB(199, 21, 133); }
        }

        [PublicAPI]
        public static UIColor VioletRed
        {
            get { return FromRGB(208, 32, 144); }
        }

        [PublicAPI]
        public static UIColor Violet
        {
            get { return FromRGB(238, 130, 238); }
        }

        [PublicAPI]
        public static UIColor Plum
        {
            get { return FromRGB(221, 160, 221); }
        }

        [PublicAPI]
        public static UIColor Orchid
        {
            get { return FromRGB(218, 112, 214); }
        }

        [PublicAPI]
        public static UIColor MediumOrchid
        {
            get { return FromRGB(186, 85, 211); }
        }

        [PublicAPI]
        public static UIColor DarkOrchid
        {
            get { return FromRGB(153, 50, 204); }
        }

        [PublicAPI]
        public static UIColor DarkViolet
        {
            get { return FromRGB(148, 0, 211); }
        }

        [PublicAPI]
        public static UIColor BlueViolet
        {
            get { return FromRGB(138, 43, 226); }
        }

        [PublicAPI]
        public static UIColor Purple
        {
            get { return FromRGB(160, 32, 240); }
        }

        [PublicAPI]
        public static UIColor MediumPurple
        {
            get { return FromRGB(147, 112, 219); }
        }

        [PublicAPI]
        public static UIColor Thistle
        {
            get { return FromRGB(216, 191, 216); }
        }
    }
}