namespace WellFired.Guacamole.Drawing.Blend
{
    public class EraseAlphaBlend
    {
        /// <summary>
        /// Perform a erase blend on two layers. This algorithm will write the result to the destination Layer.
        /// It will also return this layer, so that you can Chain operations.
        /// </summary>
        /// <param name="source">The source layer should be the layer you're trying to render on top.</param>
        /// <param name="destination">The destination layer should be the layer that already exists</param>
        public static Layer.Layer Blend(Layer.Layer source, Layer.Layer destination)
        {
            var length = destination.Size;
            for (var index = 0; index < length; )
            {
                var sourceAlpha = source.Data[index + 3]; 
                
                destination.Data[index] = PerformOptimised(source.Data[index], sourceAlpha, destination.Data[index]);
                index++;
                
                destination.Data[index] = PerformOptimised(source.Data[index], sourceAlpha, destination.Data[index]);
                index++;
                
                destination.Data[index] = PerformOptimised(source.Data[index], sourceAlpha, destination.Data[index]);
                index++;
                
                destination.Data[index] = PerformOptimised(source.Data[index], sourceAlpha, destination.Data[index]);
                index++;
            }

            return destination;
        }

        /// <summary>
        /// 1. (dR*(1-sA)) - (sR*sA)
        /// </summary>
        /// <param name="source"></param>
        /// <param name="alpha"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        private static byte PerformOptimised(byte source, byte alpha, byte destination)
        {
            // Optimised version of the AlphaBlend, without conversion to float data, with no divide.
            // http://www.codeguru.com/cpp/cpp/algorithms/general/article.php/c15989/Tip-An-Optimized-Formula-for-Alpha-Blending-Pixels.htm
            var result = (destination * (255 - alpha) - source * alpha) >> 8;

            if (result > 255)
                result = 255;
            if (result < 0)
                result = 0;
            
            return (byte)result;
        }
    }
}