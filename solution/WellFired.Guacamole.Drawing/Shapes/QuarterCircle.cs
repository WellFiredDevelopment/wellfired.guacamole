using System;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Drawing.Shapes
{
	public class QuarterCircle : IRasterizableShape
	{
		private readonly IRasterizableShape _rasterizer;

		public QuarterCircle(Quarter quarter, Vector center, double radius, double thickness)
		{
			switch (quarter)
			{
				case Quarter.TopRight:
					_rasterizer = new TopRightQuarterRasterizer(center, radius, thickness / 2.0);
					break;
				case Quarter.BottomRight:
					_rasterizer = new BottomRightQuarterRasterizer(center, radius, thickness / 2.0);
					break;
				case Quarter.BottomLeft:
					_rasterizer = new BottomLeftQuarterRasterizer(center, radius, thickness / 2.0);
					break;
				case Quarter.TopLeft:
					_rasterizer = new TopLeftQuarterRasterizer(center, radius, thickness / 2.0);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(quarter), quarter, null);
			}
		}

		public enum Quarter
		{
			TopRight,
			BottomRight,
			BottomLeft,
			TopLeft
		}

		public void Rasterize(UIImageRaw image, UIColor color)
		{
			_rasterizer.Rasterize(image, color);
		}

		public void RasterizeWithAA(UIImageRaw image, UIColor color)
		{
			_rasterizer.RasterizeWithAA(image, color);
		}

		public void RasterizeWithWidthAndAA(UIImageRaw image, UIColor color)
		{
			_rasterizer.RasterizeWithWidthAndAA(image, color);
		}
	}
}