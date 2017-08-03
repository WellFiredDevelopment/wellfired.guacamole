using System;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Drawing.Shapes
{
	public class Line : IRasterizableShape
	{
		private readonly Vector _startPoint;
		private readonly Vector _endPoint;
		private readonly double _thickness;

		public Line(Vector startPoint, Vector endPoint, double thickness)
		{
			_startPoint = startPoint;
			_endPoint = endPoint;
			_thickness = thickness;
		}

		public void Rasterize(UIImageRaw image, UIColor color)
		{
			var width = image.Width;
			var height = image.Height;

			var startX = (int) _startPoint.X;
			var startY = (int) _startPoint.Y;
			var endX = (int) _endPoint.X;
			var endY = (int) _endPoint.Y;

			PlotLine(startX, startY, endX, endY, width, height, image, color);
		}

		public static void PlotLine(int startX, int startY, int endX, int endY, int width, int height, UIImageRaw image, UIColor color)
		{
			Do(startX, startY, endX, endY, (x, y) => 
			{
				if (x >= width)
					return;
				if (y >= height)
					return;
				if (x < 0)
					return;
				if (y < 0)
					return;
				
				image[width * (height - y - 1) + x] = color;
			});
		}

		public void RasterizeWithAA(UIImageRaw image, UIColor color)
		{
			var width = image.Width;
			var height = image.Height;

			var startX = (int) _startPoint.X;
			var startY = (int) _startPoint.Y;
			var endX = (int) _endPoint.X;
			var endY = (int) _endPoint.Y;
			
			DoAA(startX, startY, endX, endY, (x, y, a) =>
			{
				if (x >= width)
					return;
				if (y >= height)
					return;
				if (x < 0)
					return;
				if (y < 0)
					return;
				
				var index = width * (height - y - 1) + x;
				color.A = 255.0f - a / 255.0f;
				image[index] = color;
			});
		}

		public void RasterizeWithWidthAndAA(UIImageRaw image, UIColor color)
		{
			var width = image.Width;
			var height = image.Height;

			var startX = (int) _startPoint.X;
			var startY = (int) _startPoint.Y;
			var endX = (int) _endPoint.X;
			var endY = (int) _endPoint.Y;
			
			DoWithWidthAndAA(startX, startY, endX, endY, (float)_thickness, (x, y, a) =>
			{
				if (x >= width)
					return;
				if (y >= height)
					return;
				if (x < 0)
					return;
				if (y < 0)
					return;
				
				var index = width * (height - y - 1) + x;
				color.A = 255.0f - a / 255.0f;
				image[index] = color;
			});
		}

		private static void Swap<T>(ref T lhs, ref T rhs)
		{
			var temp = lhs; lhs = rhs; rhs = temp;
		}
		
		private static void Do(int x0, int y0, int x1, int y1, Action<int, int> plot)
		{
			var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);

			if (steep)
			{
				Swap(ref x0, ref y0); 
				Swap(ref x1, ref y1);
			}

			if (x0 > x1)
			{
				Swap(ref x0, ref x1); 
				Swap(ref y0, ref y1);
			}

			var dX = x1 - x0;
			var dY = Math.Abs(y1 - y0);
			var err = dX / 2;
			var ystep = y0 < y1 ? 1 : -1;
			var y = y0;
 
			for (var x = x0; x <= x1; ++x)
			{
				if (steep)
					plot(y, x);
				else
					plot(x, y);
				
				err = err - dY;
				if (err < 0)
					y += ystep;  err += dX;
			}
		}

		private static void DoAA(int x0, int y0, int x1, int y1, Action<int, int, int> plot)
		{
			var dx = Math.Abs(x1 - x0);
			var sx = x0 < x1 ? 1 : -1;
			
			var dy = Math.Abs(y1 - y0);
			var sy = y0 < y1 ? 1 : -1;

			var x2 = 0;
			var e2 = 0;
			var error = dx - dy;
			var ed = dx + dy == 0 ? 1 : Math.Sqrt((float) dx * dx + (float) dy * dy);

			for (;;)
			{
				plot(x0, y0, (int)(255 * Math.Abs(e2 + dy / ed)));
				e2 = error;
				x2 = x0;
				if (e2 * 2 >= -dx)
				{
					if (x0 == x1)
						break;
					if (e2 + dy < ed)
						plot(x0, y0 + sy, (int)(255 * (e2 + dy / ed)));
					error -= dy;
					x0 += sx;
				}
				if (e2 * 2 <= dy)
				{
					if (y0 == y1)
						break;
					if(dx - e2 < ed)
						plot(x2 + sx, y0, (int)(255 * (dx - e2 / ed)));
					error += dx;
					y0 += sy;
				}
			}
		}

		private static void DoWithWidthAndAA(int x0, int y0, int x1, int y1, float wd, Action<int, int, int> plot)
		{
			int dx = Math.Abs(x1-x0), sx = x0 < x1 ? 1 : -1;
			int dy = Math.Abs(y1-y0), sy = y0 < y1 ? 1 : -1;
			int err = dx-dy, e2, x2, y2;                          /* error value e_xy */
			float ed = dx + dy == 0 ? 1 : (float)Math.Sqrt((float)dx * dx+(float)dy * dy);
			for (wd = (wd+1)/2; ; ) {                                   /* pixel loop */
				plot(x0, y0, (int)Math.Max(0,255*(Math.Abs(err-dx+dy)/ed-wd+1)));
				e2 = err; x2 = x0;
				if (2*e2 >= -dx) {                                           /* x step */
					for (e2 += dy, y2 = y0; e2 < ed*wd && (y1 != y2 || dx > dy); e2 += dx)
						plot(x0, y2 += sy, (int)Math.Max(0,255*(Math.Abs(e2)/ed-wd+1)));
					if (x0 == x1) break;
					e2 = err; err -= dy; x0 += sx;
				}
				if (2*e2 <= dy) {                                            /* y step */
					for (e2 = dx-e2; e2 < ed*wd && (x1 != x2 || dx < dy); e2 += dy)
						plot(x2 += sx, y0, (int)Math.Max(0,255*(Math.Abs(e2)/ed-wd+1)));
					if (y0 == y1) break;
					err += dx; y0 += sy;
				} }
		}
	}
}