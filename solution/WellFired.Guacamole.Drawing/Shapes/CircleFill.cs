namespace WellFired.Guacamole.Drawing.Shapes
{
    public class CircleFill : IRasterizableShape
    {
        private readonly Vector _center;
        private readonly double _radius;
        private readonly ByteColor _color;

        public CircleFill(Vector center, double radius, ByteColor color)
        {
            _center = center;
            _radius = radius;
            _color = color;
        }

        public void Rasterize(byte[] byteData, int width, int height)
        {
            // To perform a circle fill, we basically trace the outline of the circle and draw towards the center.
            Algorithms.Circle.OutlineWithAA((int)_center.X, (int)_center.Y, (int)_radius, (outerX, outerY, a) =>
            {
                Algorithms.Line.WithoutAA(outerX, outerY, (int)_center.X, (int)_center.Y, (x, y) =>
                {
                    var index = (width * (height - y - 1) + x) * 4;
                    byteData[index + 0] = _color.R;
                    byteData[index + 1] = _color.G;
                    byteData[index + 2] = _color.B;
                    byteData[index + 3] = _color.A;
                });
            });
        }
    }
}