using WellFired.Guacamole.Drawing.Blend;

namespace WellFired.Guacamole.Drawing.Shapes
{
    public class DonutFill : IRasterizableShape
    {
        private readonly Vector _center;
        private readonly double _radius;
        private readonly double _holeRadius;
        private readonly ByteColor _background;

        public DonutFill(Vector center, double radius, double holeRadius, ByteColor background)
        {
            _center = center;
            _radius = radius;
            _holeRadius = holeRadius;
            _background = background;
        }
        
        public void Rasterize(byte[] byteData, int width, int height)
        {
            var baseLayer = new Layer.Layer(byteData);
            var circle = new Layer.Layer(width, height, new Circle(_center, _radius, 1.0, _background, _background));   
            var hole = new Layer.Layer(width, height, new Circle(_center, _holeRadius, 1.0, _background, _background));
            
            Blend.Blend.Perform(hole, circle, BlendOperation.Erase);
            Blend.Blend.Perform(circle, baseLayer, BlendOperation.Normal);
        }
    }
}