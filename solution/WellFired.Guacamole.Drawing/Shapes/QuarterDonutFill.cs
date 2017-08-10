using WellFired.Guacamole.Drawing.Blend;

namespace WellFired.Guacamole.Drawing.Shapes
{
    public class QuarterDonutFill : IRasterizableShape
    {
        private readonly Vector _center;
        private readonly QuarterCircle.Quarter _quarter;
        private readonly double _radius;
        private readonly double _holeRadius;
        private readonly ByteColor _background;

        public QuarterDonutFill(QuarterCircle.Quarter quarter, Vector center, double radius, double holeRadius, ByteColor background)
        {
            _quarter = quarter;
            _center = center;
            _radius = radius;
            _holeRadius = holeRadius;
            _background = background;
        }
        
        public void Rasterize(byte[] byteData, int width, int height)
        {
            var baseLayer = new Layer.Layer(byteData);
            var circle = new Layer.Layer(width, height, new QuarterFill(_quarter, _center, _radius, _background));   
            var hole = new Layer.Layer(width, height, new QuarterFill(_quarter, _center, _holeRadius, _background));
            
            Blend.Blend.Perform(hole, circle, BlendOperation.Erase);
            Blend.Blend.Perform(circle, baseLayer, BlendOperation.Normal);
        }
    }
}