using System;
using WellFired.Guacamole.Drawing.Shapes;

namespace WellFired.Guacamole.Drawing.Algorithms
{
    public static class Circle
    {
        public static void WithoutAA(int centerX, int centerY, int radius, Action<int, int> plot)
        {
            var x = 0;
            var y = radius;

            var d = 1 - radius;
            // This decision variable is used to determine if we need to go East or South east.
            // if this d is positive, we would increment X, otherwise we would increment X and
            // decrement Y

            var firstIteration = true;

            while (x < y)
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

                plot(centerX + x, centerY + y);
                plot(centerX + x, centerY - y);
                plot(centerX - x, centerY + y);
                plot(centerX - x, centerY - y);
                plot(centerX + y, centerY + x);
                plot(centerX + y, centerY - x);
                plot(centerX - y, centerY + x);
                plot(centerX - y, centerY - x);
            }
        }

        public static void OutlineWithAA(int xm, int ym, int r, Action<int, int, byte> plot)
        {
            int x = r, y = 0; /* II. quadrant from bottom left to top right */
            int i, x2, e2, err = 2 - 2 * r;
            r = 1 - err;
            for (;;)
            {
                i = 255 * Math.Abs(err + 2 * (x + y) - 2) / r;
                plot(xm + x, ym - y, (byte) i);
                plot(xm + y, ym + x, (byte) i);
                plot(xm - x, ym + y, (byte) i);
                plot(xm - y, ym - x, (byte) i);

                if (x == 0)
                    break;

                e2 = err;
                x2 = x;

                if (err > y) // X Step condition (Outward)
                {
                    i = 255 * (err + 2 * x - 1) / r;
                    if (i < 255)
                    {
                        plot(xm + x, ym - y + 1, (byte) i);
                        plot(xm + y - 1, ym + x, (byte) i);
                        plot(xm - x, ym + y - 1, (byte) i);
                        plot(xm - y + 1, ym - x, (byte) i);
                    }
                    err -= --x * 2 - 1;
                }

                if (e2 <= x2--) // Y Step condition (Inward)
                {
                    i = 255 * (1 - 2 * y - e2) / r;
                    if (i < 255)
                    {
                        plot(xm + x2, ym - y, (byte) i);
                        plot(xm + y, ym + x2, (byte) i);
                        plot(xm - x2, ym + y, (byte) i);
                        plot(xm - y, ym - x2, (byte) i);
                    }
                    err -= --y * 2 - 1;
                }
            }
        }

        public static void QuarterOutlineWithAA(QuarterCircle.Quarter quarter, int xm, int ym, int r, Action<int, int, byte> plot)
        {
            int x = r, y = 0; /* II. quadrant from bottom left to top right */
            int i, x2, e2, err = 2 - 2 * r;
            r = 1 - err;
            for (;;)
            {
                i = 255 * Math.Abs(err + 2 * (x + y) - 2) / r;

                if (quarter == QuarterCircle.Quarter.BottomRight)
                    plot(xm + x, ym - y, (byte) i);
                if (quarter == QuarterCircle.Quarter.BottomLeft)
                    plot(xm + y, ym + x, (byte) i);
                if (quarter == QuarterCircle.Quarter.TopLeft)
                    plot(xm - x, ym + y, (byte) i);
                if (quarter == QuarterCircle.Quarter.TopRight)
                    plot(xm - y, ym - x, (byte) i);

                if (x < 0)
                    break;

                e2 = err;
                x2 = x;

                if (err > y) // X Step condition (Outward)
                {
                    i = 255 * (err + 2 * x - 1) / r;
                    if (i < 255)
                    {
                        if (quarter == QuarterCircle.Quarter.BottomRight)
                            plot(xm + x, ym - y + 1, (byte) i);
                        if (quarter == QuarterCircle.Quarter.BottomLeft)
                            plot(xm + y - 1, ym + x, (byte) i);
                        if (quarter == QuarterCircle.Quarter.TopLeft)
                            plot(xm - x, ym + y - 1, (byte) i);
                        if (quarter == QuarterCircle.Quarter.TopRight)
                            plot(xm - y + 1, ym - x, (byte) i);
                    }
                    err -= --x * 2 - 1;
                }

                if (e2 <= x2--) // Y Step condition (Inward)
                {
                    i = 255 * (1 - 2 * y - e2) / r;
                    if (i < 255)
                    {
                        if (quarter == QuarterCircle.Quarter.BottomRight)
                            plot(xm + x2, ym - y, (byte) i);
                        if (quarter == QuarterCircle.Quarter.BottomLeft)
                            plot(xm + y, ym + x2, (byte) i);
                        if (quarter == QuarterCircle.Quarter.TopLeft)
                            plot(xm - x2, ym + y, (byte) i);
                        if (quarter == QuarterCircle.Quarter.TopRight)
                            plot(xm - y, ym - x2, (byte) i);
                    }
                    err -= --y * 2 - 1;
                }
            }
        }
    }
}