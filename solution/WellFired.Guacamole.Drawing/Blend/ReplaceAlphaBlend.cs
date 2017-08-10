namespace WellFired.Guacamole.Drawing.Blend
{
    public class ReplaceAlphaBlend
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
                destination.Data[index] = source.Data[index];
                index++;
                
                destination.Data[index] = source.Data[index];
                index++;
                
                destination.Data[index] = source.Data[index];
                index++;
                
                destination.Data[index] = source.Data[index];
                index++;
            }

            return destination;
        }
    }
}