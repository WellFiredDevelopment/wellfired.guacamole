using System;
using JetBrains.Annotations;

namespace WellFired.Guacamole.Drawing.Algorithms
{
    public class Line
    {
	    [PublicAPI]
		private static void Swap<T>(ref T lhs, ref T rhs)
		{
			var temp = lhs; lhs = rhs; rhs = temp;
		}
		
		public static void WithoutAA(int x0, int y0, int x1, int y1, Action<int, int> plot)
		{
			int dx =  Math.Abs(x1-x0), sx = x0<x1 ? 1 : -1;
			int dy = -Math.Abs(y1-y0), sy = y0<y1 ? 1 : -1;
			int err = dx+dy, e2;
			for (;;)
			{
				plot(x0, y0);
				e2 = 2 * err;
				if (e2 >= dy)
				{
					if (x0 == x1) break;
					err += dy;
					x0 += sx;
				}
				if (e2 <= dx)
				{
					if (y0 == y1) break;
					err += dx;
					y0 += sy;
				}
			}
		}

	    public static void WithAA(int x0, int y0, int x1, int y1, Action<int, int, byte> plot)
		{
			var dx = Math.Abs(x1 - x0);
			var sx = x0 < x1 ? 1 : -1;
			
			var dy = Math.Abs(y1 - y0);
			var sy = y0 < y1 ? 1 : -1;

			var e2 = 0;
			var error = dx - dy;
			var ed = dx + dy == 0 ? 1 : Math.Sqrt((float) dx * dx + (float) dy * dy);

			for (;;)
			{
				plot(x0, y0, (byte)(255 * Math.Abs(e2 + dy / ed)));
				e2 = error;
				var x2 = x0;
				if (e2 * 2 >= -dx)
				{
					if (x0 == x1)
						break;
					if (e2 + dy < ed)
						plot(x0, y0 + sy, (byte)(255 * (e2 + dy / ed)));
					error -= dy;
					x0 += sx;
				}
				if (e2 * 2 <= dy)
				{
					if (y0 == y1)
						break;
					if(dx - e2 < ed)
						plot(x2 + sx, y0, (byte)(255 * (dx - e2 / ed)));
					error += dx;
					y0 += sy;
				}
			}
		}

	    public static void DoWithWidthAndAA(int x0, int y0, int x1, int y1, double wd, Action<int, int, byte> plot)
		{
			int dx = Math.Abs(x1-x0), sx = x0 < x1 ? 1 : -1;
			int dy = Math.Abs(y1-y0), sy = y0 < y1 ? 1 : -1;
			int err = dx-dy, e2, x2, y2;                          /* error value e_xy */
			var ed = dx + dy == 0 ? 1 : (float)Math.Sqrt((float)dx * dx+(float)dy * dy);
			for (wd = (wd+1)/2; ; ) {                                   /* pixel loop */
				plot(x0, y0, (byte)Math.Max(0,255*(Math.Abs(err-dx+dy)/ed-wd+1)));
				e2 = err; x2 = x0;
				if (2*e2 >= -dx) {                                           /* x step */
					for (e2 += dy, y2 = y0; e2 < ed*wd && (y1 != y2 || dx > dy); e2 += dx)
						plot(x0, y2 += sy, (byte)Math.Max(0,255*(Math.Abs(e2)/ed-wd+1)));
					if (x0 == x1) break;
					e2 = err; err -= dy; x0 += sx;
				}
				if (2*e2 <= dy) {                                            /* y step */
					for (e2 = dx-e2; e2 < ed*wd && (x1 != x2 || dx < dy); e2 += dy)
						plot(x2 += sx, y0, (byte)Math.Max(0,255*(Math.Abs(e2)/ed-wd+1)));
					if (y0 == y1) break;
					err += dx; y0 += sy;
				} }
		}

	    public static void DoWithWidth(int x0, int y0, int x1, int y1, double wd, Action<int, int> plot)
	    {
		    int dx = Math.Abs(x1-x0), sx = x0 < x1 ? 1 : -1;
		    int dy = Math.Abs(y1-y0), sy = y0 < y1 ? 1 : -1;
		    int err = dx-dy, e2, x2, y2;                          /* error value e_xy */
		    var ed = dx + dy == 0 ? 1 : (float)Math.Sqrt((float)dx * dx+(float)dy * dy);
		    for (wd = (wd+1)/2; ; ) {                                   /* pixel loop */
			    plot(x0, y0);
			    e2 = err; x2 = x0;
			    if (2*e2 >= -dx) {                                           /* x step */
				    for (e2 += dy, y2 = y0; e2 < ed*wd && (y1 != y2 || dx > dy); e2 += dx)
					    plot(x0, y2 += sy);
				    if (x0 == x1) break;
				    e2 = err; err -= dy; x0 += sx;
			    }
			    if (2*e2 <= dy) {                                            /* y step */
				    for (e2 = dx-e2; e2 < ed*wd && (x1 != x2 || dx < dy); e2 += dy)
					    plot(x2 += sx, y0);
				    if (y0 == y1) break;
				    err += dx; y0 += sy;
			    } }
	    }
    }
}