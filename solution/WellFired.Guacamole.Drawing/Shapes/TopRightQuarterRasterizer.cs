using System;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Drawing.Shapes
{
    public class TopRightQuarterRasterizer : IRasterizableShape
    {
        private readonly double _centerX;
        private readonly double _centerY;
        private readonly double _radius;
        private readonly double _thickness;

        public TopRightQuarterRasterizer(Vector center, double radius, double thickness)
        {
            _centerX = center.X;
            _centerY = center.Y;
            _radius = radius;
            _thickness = thickness;
        }

        public void Rasterize(UIImageRaw image, UIColor color)
        {
            var width = image.Width;
            var height = image.Height;
			
            Do((int)_centerX, (int)_centerY, (int)_radius, (x, y) => {
                image[width * (height - y - 1) + x] = color;
            });
        }

        public void RasterizeWithAA(UIImageRaw image, UIColor color)
        {
            var width = image.Width;
            var height = image.Height;
			
            DoAA((int)_centerX, (int)_centerY, (int)_radius, (x, y, a) => {
                var index = width * (height - y - 1) + x;
                color.A = 1.0f - a / 255.0f;
                image[index] = color;
            });
        }

        public void RasterizeWithWidthAndAA(UIImageRaw image, UIColor color)
        {
            var innerCircle = new TopRightQuarterRasterizer(new Vector(_centerX, _centerY), _radius - _thickness, _thickness);
            RasterizeWithAA(image, color);
            innerCircle.RasterizeWithAA(image, color);
        }

        private static void Do(int centerX, int centerY, int radius, Action<int, int> plot)
        {
            var x = 0;
            var y = radius;
            
            var d = 1 - radius; // This decision variable is used to determine if we need to go East or South east.
            // if this d is positive, we would increment X, otherwise we would increment X and
            // decrement Y

            var firstIteration = true;
            
            while(x < y)
            {
                if (!firstIteration)
                {
                    if (d < 0)
                    {
                        d = d + 2 * x + 3;
                        x += 1;
                    }
                    else
                    {
                        d = d + 2 * (x - y) + 5;
                        x += 1;
                        y -= 1;
                    }
                }
                firstIteration = false;

                plot(centerX + x, centerY - y);
                plot(centerX + y, centerY - x);
            }
        }

        private static void DoAA(int xm, int ym, int r, Action<int, int, int> plot)
        {
            int x = r, y = 0;    /* II. quadrant from bottom left to top right */
            int i, x2, e2, err = 2 - 2 * r;
            r = 1 - err;
            for(;;)
            {
                i = 255 * Math.Abs(err + 2 * (x + y) - 2) / r;
                plot(xm - y, ym - x, i);

                if (x == 0)
                    break;
                
                e2 = err; 
                x2 = x;

                if (err > y) // X Step condition (Outward)
                {
                    i = 255 * (err + 2 * x - 1) / r;
                    if (i < 255) 
                    {
                        plot(xm-y+1, ym-x, i);
                    }
                    err -= --x*2-1;
                }

                if (e2 <= x2--) // Y Step condition (Inward)
                {
                    i = 255 * (1 - 2 * y - e2) / r;
                    if (i < 255) 
                    {
                        plot(xm-y, ym-x2, i);
                    }
                    err -= --y * 2 - 1;
                }   
            }
        }
    }
}