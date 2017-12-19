using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Drawing.Blend
{
    public static class AlphaBlend
    {
        /// <summary>
        /// Perform a normal blend on two layers. This algorithm will write the result to the destination Layer.
        /// It will also return this layer, so that you can Chain operations.
        /// </summary>
        /// <param name="source">The source layer should be the layer you're trying to render on top.</param>
        /// <param name="destination">The destination layer should be the layer that already exists</param>
        public static Layer.Layer Blend(Layer.Layer source, Layer.Layer destination)
        {
            var length = destination.Size;
            for (var index = 0; index < length; )
            {
                var alpha = source.Data[index + 3]; 
                
                destination.Data[index] = Perform(source.Data[index], destination.Data[index], alpha);
                index++;
                
                destination.Data[index] = Perform(source.Data[index], destination.Data[index], alpha);
                index++;
                
                destination.Data[index] = Perform(source.Data[index], destination.Data[index], alpha);
                index++;
                
                destination.Data[index] = Perform(source.Data[index], destination.Data[index], alpha);
                index++;
            }

            return destination;
        }
        
        /// <summary>
        /// Performs a normal Alpha blend.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="alpha"></param>
        /// <returns></returns>
        [PublicAPI]
        public static byte Perform(byte source, byte destination, byte alpha)
        {
            // Optimised version of the AlphaBlend, without conversion to float data, with no divide.
            // http://www.codeguru.com/cpp/cpp/algorithms/general/article.php/c15989/Tip-An-Optimized-Formula-for-Alpha-Blending-Pixels.htm
            var result = (((source * alpha) + (destination * (255 - alpha))) >> 8);

            if (result > 255)
                result = 255;
            if (result < 0)
                result = 0;
            
            return (byte)result;
        }
    }
}