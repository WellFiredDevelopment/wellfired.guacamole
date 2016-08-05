using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
    public partial struct UIColor
    {
        [PublicAPI]
        public static UIColor Black
        {
            get { return FromRGB(0, 0, 0); }
        }

        [PublicAPI]
        public static UIColor DarkSlateGrey
        {
            get { return FromRGB(49, 79, 79); }
        }

        [PublicAPI]
        public static UIColor DimGrey
        {
            get { return FromRGB(105, 105, 105); }
        }

        [PublicAPI]
        public static UIColor SlateGrey
        {
            get { return FromRGB(112, 138, 144); }
        }

        [PublicAPI]
        public static UIColor LightSlateGrey
        {
            get { return FromRGB(119, 136, 153); }
        }

        [PublicAPI]
        public static UIColor Grey
        {
            get { return FromRGB(190, 190, 190); }
        }

        [PublicAPI]
        public static UIColor LightGrey
        {
            get { return FromRGB(211, 211, 211); }
        }
    }
}