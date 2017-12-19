namespace WellFired.Guacamole.Drawing.Shapes
{
    public class QuarterOutline : IRasterizableShape
    {
        private readonly QuarterCircle.Quarter _quarter;
        private readonly Vector _center;
        private readonly double _radius;
        private readonly ByteColor _color;

        public QuarterOutline(QuarterCircle.Quarter quarter, Vector center, double radius, ByteColor color)
        {
            _quarter = quarter;
            _center = center;
            _radius = radius;
            _color = color;
        }

        public void Rasterize(byte[] byteData, int width, int height)
        {
            Algorithms.Circle.QuarterOutlineWithAA(_quarter, (int) _center.X, (int) _center.Y, (int) _radius, (x, y, a) =>
            {
                if (x >= width)
                    return;
                if (y >= height)
                    return;
                if (x < 0)
                    return;
                if (y < 0)
                    return;
                
                var index = (width * (height - y - 1) + x) * 4;

                a = (byte) (255 - a);
                byteData[index + 0] = _color.R;
                byteData[index + 1] = _color.B;
                byteData[index + 2] = _color.G;
                byteData[index + 3] = a;
            });
        }
    }
}