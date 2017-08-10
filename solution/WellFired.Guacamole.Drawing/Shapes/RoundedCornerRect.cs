using WellFired.Guacamole.Data;
using WellFired.Guacamole.Drawing.Extensions;

namespace WellFired.Guacamole.Drawing.Shapes
{
    public class RoundedCornerRect : IRasterizableShape
    {
        private readonly Rect _rect;
        private readonly double _radius;
        private readonly double _thickness;
        private readonly ByteColor _background;
        private readonly ByteColor _outline;
        private readonly CornerMask _cornerMask;
        private readonly OutlineMask _outlineMask;

        public RoundedCornerRect(Rect rect, double radius, double thickness, ByteColor background, ByteColor outline, CornerMask cornerMask, OutlineMask outlineMask)
        {
            _rect = rect;
            _radius = radius;
            _thickness = thickness;
            _background = background;
            _outline = outline;
            _cornerMask = cornerMask;
            _outlineMask = outlineMask;
        }

        public void Rasterize(byte[] byteData, int width, int height)
        {   
            if (_cornerMask.Is(CornerMask.TopLeft))
            {
                new Quarter(QuarterCircle.Quarter.TopLeft, new Vector(_rect.X + _radius, _rect.Y + _radius), _radius, _thickness, _background, _outline)
                    .Rasterize(byteData, width, height);
            }
            
            if (_cornerMask.Is(CornerMask.TopRight))
            {
                new Quarter(QuarterCircle.Quarter.TopRight, new Vector(_rect.X + _rect.Width - _radius, _rect.Y + _radius), _radius, _thickness, _background, _outline)
                    .Rasterize(byteData, width, height);
            }
            
            if (_cornerMask.Is(CornerMask.BottomRight))
            {
                new Quarter(QuarterCircle.Quarter.BottomRight, new Vector(_rect.X + _rect.Width - _radius, _rect.Y + _rect.Height - _radius), _radius, _thickness, _background, _outline)
                    .Rasterize(byteData, width, height);
            }
            
            if (_cornerMask.Is(CornerMask.BottomLeft))
            {
                new Quarter(QuarterCircle.Quarter.BottomLeft, new Vector(_rect.X + _radius, _rect.Y + _rect.Height - _radius), _radius, _thickness, _background, _outline)
                    .Rasterize(byteData, width, height);
            }
            
            var halfThickness = _thickness / 2.0;
            if (_outlineMask.Is(OutlineMask.Top))
            {
                var startPoint = !_cornerMask.Is(CornerMask.TopLeft) 
                    ? new Vector(0, _rect.Y + halfThickness) 
                    : new Vector(_radius + 1, _rect.Y + halfThickness);
                
                var endPoint = !_cornerMask.Is(CornerMask.TopRight) 
                    ? new Vector(_rect.X + _rect.Width, _rect.Y + halfThickness) 
                    : new Vector(_rect.X + _rect.Width - _radius, _rect.Y + halfThickness);
                
                new Line(startPoint, endPoint, _thickness, _outline).Rasterize(byteData, width, height);
            }
            
            if (_outlineMask.Is(OutlineMask.Right))
            {
                var startPoint = !_cornerMask.Is(CornerMask.TopRight) 
                    ? new Vector(_rect.X + _rect.Width, 0) 
                    : new Vector(_rect.X + _rect.Width, _radius + 1);
                
                var endPoint = !_cornerMask.Is(CornerMask.BottomRight)
                    ? new Vector(_rect.X + _rect.Width, _rect.Y + _rect.Height - 1)
                    : new Vector(_rect.X + _rect.Width, _rect.Y + _rect.Height - _radius - 1);
                
                new Line(startPoint, endPoint, _thickness, _outline).Rasterize(byteData, width, height);
            }
            
            if (_outlineMask.Is(OutlineMask.Bottom))
            {
                var startPoint = !_cornerMask.Is(CornerMask.BottomRight)
                    ? new Vector(_rect.X + _rect.Width, _rect.Y + _rect.Height)
                    : new Vector(_rect.X + _rect.Width - _radius - 1, _rect.Y + _rect.Height);
                
                var endPoint = !_cornerMask.Is(CornerMask.BottomLeft)
                    ? new Vector(0, _rect.Y + _rect.Height)
                    : new Vector(_radius, _rect.Y + _rect.Height);
                
                new Line(startPoint, endPoint, _thickness, _outline).Rasterize(byteData, width, height);
            }
            
            if (_outlineMask.Is(OutlineMask.Left))
            {
                var startPoint = !_cornerMask.Is(CornerMask.BottomLeft)
                    ? new Vector(_rect.X + halfThickness, _rect.Y + _rect.Height)
                    : new Vector(_rect.X + halfThickness, _rect.Y + _rect.Height - _radius - 1);
                
                var endPoint = !_cornerMask.Is(CornerMask.TopLeft)
                    ? new Vector(_rect.X + halfThickness, _rect.Y)
                    : new Vector(_rect.X + halfThickness, _radius + 1);
                
                new Line(startPoint, endPoint, _thickness, _outline).Rasterize(byteData, width, height);
            }

            new ImageFill().Fill(new RawImage { Data = byteData, Width = width, Height = height }, new Pixel(width / 2, height / 2), _background, FillStyle.Linear);
        }
    }
}