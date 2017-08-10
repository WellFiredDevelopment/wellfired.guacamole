namespace WellFired.Guacamole.Drawing.Layer
{
    public class Operation
    {
        /// <summary>
        /// Subtracts one layer from another
        /// It will also return this layer, so that you can Chain operations.
        /// </summary>
        /// <param name="source">The source layer should be the layer you're trying to render on top.</param>
        /// <param name="destination">The destination layer should be the layer that already exists</param>
        public static Layer Subtract(Layer source, Layer destination)
        {
            var length = destination.Size;
            for (var index = 0; index < length; )
            {
                destination.Data[index] = SubtractOperation(source.Data[index], destination.Data[index]);
                index++;
                
                destination.Data[index] = SubtractOperation(source.Data[index], destination.Data[index]);
                index++;
                
                destination.Data[index] = SubtractOperation(source.Data[index], destination.Data[index]);
                index++;
                
                destination.Data[index] = SubtractOperation(source.Data[index], destination.Data[index]);
                index++;
            }

            return destination;
        }

        private static byte SubtractOperation(byte source, byte destination)
        {
            return (byte)(destination - source);
        }
    }
}