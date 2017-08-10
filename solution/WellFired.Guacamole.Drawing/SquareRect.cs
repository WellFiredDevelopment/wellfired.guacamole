using WellFired.Guacamole.Data;
using WellFired.Guacamole.Drawing.Extensions;
using WellFired.Guacamole.Drawing.Shapes;

namespace WellFired.Guacamole.Drawing
{
    public class SquareRect : IRasterizableShape
    {
        private readonly Rect _rect;
        private readonly double _thickness;
        private readonly ByteColor _background;
        private readonly ByteColor _outline;
        private readonly OutlineMask _outlineMask;

        public SquareRect(Rect rect, double thickness, ByteColor background, ByteColor outline, OutlineMask outlineMask)
        {
            _rect = rect;
            _thickness = thickness;
            _background = background;
            _outline = outline;
            _outlineMask = outlineMask;
        }

        public void Rasterize(byte[] byteData, int width, int height)
        {
            var halfThickness = _thickness / 2.0;
            if (_outlineMask.Is(OutlineMask.Top))
            {
                var startPoint = new Vector(0, _rect.Y + halfThickness);
                var endPoint = new Vector(_rect.X + _rect.Width, _rect.Y + halfThickness);
                
                new Line(startPoint, endPoint, _thickness, _outline).Rasterize(byteData, width, height);
            }
            
            if (_outlineMask.Is(OutlineMask.Right))
            {
                var startPoint = new Vector(_rect.X + _rect.Width, 0);
                var endPoint = new Vector(_rect.X + _rect.Width, _rect.Y + _rect.Height - 1);
                
                new Line(startPoint, endPoint, _thickness, _outline).Rasterize(byteData, width, height);
            }
            
            if (_outlineMask.Is(OutlineMask.Bottom))
            {
                var startPoint = new Vector(_rect.X + _rect.Width, _rect.Y + _rect.Height);
                var endPoint = new Vector(0, _rect.Y + _rect.Height);
                
                new Line(startPoint, endPoint, _thickness, _outline).Rasterize(byteData, width, height);
            }
            
            if (_outlineMask.Is(OutlineMask.Left))
            {
                var startPoint = new Vector(_rect.X + halfThickness, _rect.Y + _rect.Height);
                var endPoint = new Vector(_rect.X + halfThickness, _rect.Y);
                
                new Line(startPoint, endPoint, _thickness, _outline).Rasterize(byteData, width, height);
            }

            new ImageFill().Fill(new RawImage { Data = byteData, Width = width, Height = height }, new Pixel(width / 2, height / 2), _background, FillStyle.Linear);
        }
    }
}