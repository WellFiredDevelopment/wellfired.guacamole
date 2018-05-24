using System;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Views
{
    public static class SizingHelper
    {
        public static float GetImportantSize(OrientationOptions orientation, UIRect rectRequest)
        {
            switch (orientation)
            {
                case OrientationOptions.Horizontal:
                    return rectRequest.Width;
                case OrientationOptions.Vertical:
                    return rectRequest.Height;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public static UISize ZeroUnImportantSize(OrientationOptions orientation, UISize size)
        {
            switch (orientation)
            {
                case OrientationOptions.Horizontal:
                    return UISize.Of(size.Width, 0);
                case OrientationOptions.Vertical:
                    return UISize.Of(0, size.Height);
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public static float GetImportantSize(OrientationOptions orientation, UISize size)
        {
            switch (orientation)
            {
                case OrientationOptions.Horizontal:
                    return size.Width;
                case OrientationOptions.Vertical:
                    return size.Height;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }

        public static float GetImportantValue(OrientationOptions orientation, float x, float y)
        {
            switch (orientation)
            {
                case OrientationOptions.Horizontal:
                    return x;
                case OrientationOptions.Vertical:
                    return y;
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), orientation, null);
            }
        }
    }
}