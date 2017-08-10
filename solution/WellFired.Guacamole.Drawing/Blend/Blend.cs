using System;
using WellFired.Guacamole.Drawing.Layer;

namespace WellFired.Guacamole.Drawing.Blend
{
    public static class Blend
    {
        private delegate byte BlendMethod(byte source, byte destination, byte alpha);

        /// <summary>
        /// Perform a blend on two layers. This algorithm will write the result to the destination Layer.
        /// It will also return this layer, so that you can Chain operations.
        /// </summary>
        /// <param name="source">The source layer should be the layer you're trying to render on top.</param>
        /// <param name="destination">The destination layer should be the layer that already exists</param>
        /// <param name="blendOperation">The blend operation we will perform between these two layers.</param>
        public static Layer.Layer Perform(Layer.Layer source, Layer.Layer destination, BlendOperation blendOperation)
        {
            switch (blendOperation)
            {
                case BlendOperation.Normal:
                    return AlphaBlend.Blend(source, destination);
                case BlendOperation.Erase:
                    return EraseAlphaBlend.Blend(source, destination);
                case BlendOperation.Replace:
                    return ReplaceAlphaBlend.Blend(source, destination);
                case BlendOperation.MaxRgbBlendABlend:
                    return MaxRgbBlendABlend.Blend(source, destination);
                default:
                    throw new ArgumentOutOfRangeException(nameof(blendOperation), blendOperation, null);
            }
        }
    }
}