using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
    public partial struct UIColor
    {
        [PublicAPI]
        public static UIColor PaleGoldenrod
        {
            get { return FromRGB(238, 232, 170); }
        }

        [PublicAPI]
        public static UIColor RosyBrown
        {
            get { return FromRGB(188, 143, 143); }
        }

        [PublicAPI]
        public static UIColor IndianRed
        {
            get { return FromRGB(205, 92, 92); }
        }

        [PublicAPI]
        public static UIColor SaddleBrown
        {
            get { return FromRGB(139, 69, 19); }
        }

        [PublicAPI]
        public static UIColor Sienna
        {
            get { return FromRGB(160, 82, 45); }
        }

        [PublicAPI]
        public static UIColor Peru
        {
            get { return FromRGB(205, 133, 63); }
        }

        [PublicAPI]
        public static UIColor Burlywood
        {
            get { return FromRGB(222, 184, 135); }
        }

        [PublicAPI]
        public static UIColor Beige
        {
            get { return FromRGB(245, 245, 220); }
        }

        [PublicAPI]
        public static UIColor Wheat
        {
            get { return FromRGB(245, 222, 179); }
        }

        [PublicAPI]
        public static UIColor SandyBrown
        {
            get { return FromRGB(244, 164, 96); }
        }

        [PublicAPI]
        public static UIColor Tan
        {
            get { return FromRGB(210, 180, 140); }
        }

        [PublicAPI]
        public static UIColor Chocolate
        {
            get { return FromRGB(210, 105, 30); }
        }

        [PublicAPI]
        public static UIColor Firebrick
        {
            get { return FromRGB(178, 34, 34); }
        }

        [PublicAPI]
        public static UIColor Brown
        {
            get { return FromRGB(165, 42, 42); }
        }
    }
}