namespace WellFired.Guacamole.Drawing.Shapes
{
    public class CircleOutline : IRasterizableShape
    {
        private readonly Vector _center;
        private readonly double _radius;
        private readonly ByteColor _color;

        public CircleOutline(Vector center, double radius, ByteColor color)
        {
            _center = center;
            _radius = radius;
            _color = color;
        }

        public void Rasterize(byte[] byteData, int width, int height)
        {
            Algorithms.Circle.OutlineWithAA((int) _center.X, (int) _center.Y, (int) _radius, (x, y, a) =>
            {
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