using WellFired.Guacamole.Drawing.Blend;

namespace WellFired.Guacamole.Drawing.Shapes
{
    public class Quarter : IRasterizableShape
    {
        private readonly QuarterCircle.Quarter _quarter;
        private readonly Vector _center;
        private readonly double _radius;
        private readonly double _thickness;
        private readonly ByteColor _background;
        private readonly ByteColor _outline;

        public Quarter(QuarterCircle.Quarter quarter, Vector center, double radius, double thickness, ByteColor background, ByteColor outline)
        {
            _quarter = quarter;
            _center = center;
            _radius = radius;
            _thickness = thickness;
            _background = background;
            _outline = outline;
        }

        public void Rasterize(byte[] byteData, int width, int height)
        {
            if (_background == _outline || _thickness < 1.0)
                RasterizeWithNoOutline(byteData, width, height);
            else if (_thickness < 2.0)
                RasterizeWithThinOutline(byteData, width, height);
            else
                RasterizeWithEverything(byteData, width, height);   
        }

        private void RasterizeWithNoOutline(byte[] byteData, int width, int height)
        {
            var baseLayer = new Layer.Layer(byteData);
            var outlineLayer = new Layer.Layer(width, height, new QuarterOutline(_quarter, _center, _radius, _background));
            var fillLayer = new Layer.Layer(width, height, new QuarterFill(_quarter, _center, _radius - 1, _background));
            
            Blend.Blend.Perform(fillLayer, baseLayer, BlendOperation.Normal);
            Blend.Blend.Perform(outlineLayer, baseLayer, BlendOperation.Normal);
        }

        private void RasterizeWithThinOutline(byte[] byteData, int width, int height)
        {
            var baseLayer = new Layer.Layer(byteData);
            var outlineLayer = new Layer.Layer(width, height, new QuarterOutline(_quarter, _center, _radius, _outline));
            var fillLayer = new Layer.Layer(width, height, new QuarterFill(_quarter, _center, _radius, _background));
            
            Blend.Blend.Perform(fillLayer, baseLayer, BlendOperation.Normal);
            Blend.Blend.Perform(outlineLayer, baseLayer, BlendOperation.Normal);
        }

        private void RasterizeWithEverything(byte[] byteData, int width, int height)
        {
            var thicknessModifier = _thickness * 0.75f;
            var baseLayer = new Layer.Layer(byteData);
            var outlineLayer = new Layer.Layer(width, height, new QuarterDonutFill(_quarter, _center, _radius, _radius - thicknessModifier, _outline));
            var fillLayer = new Layer.Layer(width, height, new QuarterFill(_quarter, _center, _radius - thicknessModifier + 1, _background));
            
            Blend.Blend.Perform(fillLayer, baseLayer, BlendOperation.Normal);
            Blend.Blend.Perform(outlineLayer, baseLayer, BlendOperation.Normal); 
        }
    }
}