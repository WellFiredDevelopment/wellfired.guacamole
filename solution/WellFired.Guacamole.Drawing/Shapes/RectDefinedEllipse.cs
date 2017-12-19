using System;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Drawing.Shapes
{
    public class RectDefinedEllipse : IRasterizableShape
    {
        private readonly Rect _rect;
        //private readonly double _thickness;

        public RectDefinedEllipse(Rect rect, double thickness)
        {
            _rect = rect;
            //_thickness = thickness;
        }

        public void Rasterize(RawImage image, UIColor color)
        {
            var width = image.Width;
            var height = image.Height;
            var x0 = _rect.X;
            var x1 = _rect.X + _rect.Width;
            var y0 = _rect.Y;
            var y1 = _rect.Y + _rect.Height;

            Do((int) x0, (int) x1, (int) y0, (int) y1, (x, y) =>
            {
                if (x > width)
                    return;
                if (y > height)
                    return;
                if (x < 0)
                    return;
                if (y < 0)
                    return;
                //var index = width * y + x;
                //image[index] = color;
            });
        }

        public void RasterizeWithAA(RawImage image, UIColor color)
        {
            var width = image.Width;
            var height = image.Height;
            var x0 = _rect.X;
            var x1 = _rect.X + _rect.Width;
            var y0 = _rect.Y;
            var y1 = _rect.Y + _rect.Height;

            DoAA((int) x0, (int) x1, (int) y0, (int) y1, width, height, image, color, (x, y, a) =>
            {
                if (x > width)
                    return;
                if (y > height)
                    return;
                if (x < 0)
                    return;
                if (y < 0)
                    return;
                //var index = width * (height - y - 1) + x;
                //color.A = 1.0f - a / 255.0f;
                //image[index] = color;
            });
        }

        public void RasterizeWithWidthAndAA(RawImage image, UIColor color)
        {
            RasterizeWithAA(image, color);
        }

        /// <summary>
        /// Taken from the Bresenham paper at http://members.chello.at/easyfilter/Bresenham.pdf
        /// </summary>
        /// <param name="x0">The left most x parameter that defines the rectangle</param>
        /// <param name="x1">The right most x parameter that defines the rectangle</param>
        /// <param name="y0">The bottom most y parameter that defines the rectangle</param>
        /// <param name="y1">The top most y parameter that defines the rectangle</param>
        /// <param name="plot"></param>
        private static void Do(int x0, int x1, int y0, int y1, Action<int, int> plot)
        {
            var a = Math.Abs(x1 - x0);
            var b = Math.Abs(y1 - y0);
            var b1 = 2.5;

            var dx = 4.0 * (1.0 - a) * b * b;
            var dy = 4.0 * (b1 + 1.0) * a * a;
            var error = dx + dy + b1 * a * a;
            double e2 = 0;

            if (x0 > x1)
            {
                x0 = x1;
                x1 += a;
            }

            if (y0 > y1)
                y0 = y1;

            y0 = y0 + (b + 1) / 2;
            y1 = (int) (y0 - b1);
            a = 8 * a * a;
            b1 = 8 * b * b;

            do
            {
                plot(x1, y0);
                plot(x0, y0);
                plot(x0, y1);
                plot(x1, y1);

                e2 = 2.0 * error;

                if (e2 <= dy)
                {
                    y0++;
                    y1--;
                    dy = dy + a;
                    error = error + dy;
                }

                if (e2 >= dx || 2 * error > dy)
                {
                    x0++;
                    x1--;
                    dx = dx + b1;
                    error = error + dx;
                }
            } while (x0 <= x1);

            while (y0 - y1 <= b)
            {
                plot(x0 - 1, y0);
                y0 = y0 + 1;
                plot(x1 + 1, y0);
                plot(x0 - 1, y1);
                y1 = y1 - 1;
                plot(x1 + 1, y1);
            }
        }

        /// <summary>
        /// Taken from the Bresenham paper at http://members.chello.at/easyfilter/Bresenham.pdf
        /// </summary>
        private static void DoAA(int x0, int x1, int y0, int y1, int width, int height, RawImage image, UIColor color, Action<int, int, int> plot)
        {
            long a = Math.Abs(x1 - x0), b = Math.Abs(y1 - y0), b1 = b & 1;
            float dx = (float) (4 * (a - 1.0) * b * b), dy = 4 * (b1 + 1) * a * a;
            float ed, i, err = b1 * a * a - dx + dy;
            bool f;

            if (a == 0 || b == 0)
            {
                throw new Exception();
                // [TODO] : Replace this with the new Algorithms.line version.
//              Line.Plot(x0, y0, x1, y1, width, height, image.Data, color);
//              return;
            }

            if (x0 > x1)
            {
                x0 = x1;
                x1 += (int) a;
            }
            if (y0 > y1) y0 = y1;
            y0 += (int) (b + 1) / 2;
            y1 = y0 - (int) b1;
            a = 8 * a * a;
            b1 = 8 * b * b;
            for (;;)
            {
                i = Math.Min(dx, dy);
                ed = Math.Max(dx, dy);
                if (y0 == y1 + 1 && err > dy && a > b1) ed = (float) (255.0 * 4.0 / a); /* x-tip */
                else ed = 255 / (ed + 2 * ed * i * i / (4 * ed * ed + i * i)); /* approximation */
                i = ed * Math.Abs(err + dx - dy); /* get intensity value by pixel error */
                plot(x0, y0, (int) i);
                plot(x0, y1, (int) i);
                plot(x1, y0, (int) i);
                plot(x1, y1, (int) i);
                f = 2 * err + dy >= 0;
                if (f)
                {
                    /* x step, remember condition */
                    if (x0 >= x1) break;
                    i = ed * (err + dx);
                    if (i < 255)
                    {
                        plot(x0, y0 + 1, (int) i);
                        plot(x0, y1 - 1, (int) i);
                        plot(x1, y0 + 1, (int) i);
                        plot(x1, y1 - 1, (int) i);
                    } /* do error increment later since values are still needed */
                }
                if (2 * err <= dx)
                {
                    i = ed * (dy - err);
                    if (i < 255)
                    {
                        plot(x0 + 1, y0, (int) i);
                        plot(x1 - 1, y0, (int) i);
                        plot(x0 + 1, y1, (int) i);
                        plot(x1 - 1, y1, (int) i);
                    }
                    y0++;
                    y1--;
                    err += dy += a;
                }

                if (f)
                {
                    x0++;
                    x1--;
                    err -= dx -= b1;
                }
            }
            if (--x0 == x1++) /* too early stop of flat ellipses */
                while (y0 - y1 < b)
                {
                    i = 255 * 4 * Math.Abs(err + dx) / b1; /* -> finish tip of ellipse */
                    plot(x0, ++y0, (int) i);
                    plot(x1, y0, (int) i);
                    plot(x0, --y1, (int) i);
                    plot(x1, y1, (int) i);
                    err += dy += a;
                }
        }

        public void Rasterize(byte[] byteData, int width, int height)
        {
            throw new NotImplementedException();
        }
    }
}