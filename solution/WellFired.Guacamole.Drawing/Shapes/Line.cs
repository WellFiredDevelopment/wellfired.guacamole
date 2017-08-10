namespace WellFired.Guacamole.Drawing.Shapes
{
	public class Line : IRasterizableShape
	{
		private readonly Vector _startPoint;
		private readonly Vector _endPoint;
		private readonly double _thickness;
		private readonly ByteColor _outline;

		public Line(Vector startPoint, Vector endPoint, double thickness, ByteColor outline)
		{
			_startPoint = startPoint;
			_endPoint = endPoint;
			_thickness = thickness;
			_outline = outline;
		}

		public void Rasterize(byte[] byteData, int width, int height)
		{
			if(_thickness < 2.0f)
				RasterizeWithoutThickness(byteData, width, height);
			else
				RasterizeWithThickness(byteData, width, height);
		}

		private void RasterizeWithoutThickness(byte[] byteData, int width, int height)
		{
			Algorithms.Line.WithoutAA((int)_startPoint.X, (int)_startPoint.Y, (int)_endPoint.X, (int)_endPoint.Y, (x, y) => 
			{
				if (x >= width)
					return;
				if (y >= height)
					return;
				if (x < 0)
					return;
				if (y < 0)
					return;
				
				var index = (width * (height - y - 1) + x) * 4;

				byteData[index + 0] = _outline.R;
				byteData[index + 1] = _outline.B;
				byteData[index + 2] = _outline.G;
				byteData[index + 3] = 255;
			});
		}

		private void RasterizeWithThickness(byte[] byteData, int width, int height)
		{
			Algorithms.Line.DoWithWidth((int)_startPoint.X, (int)_startPoint.Y, (int)_endPoint.X, (int)_endPoint.Y, _thickness, (x, y) => 
			{
				if (x >= width)
					return;
				if (y >= height)
					return;
				if (x < 0)
					return;
				if (y < 0)
					return;
				
				var index = (width * (height - y - 1) + x) * 4;

				byteData[index + 0] = _outline.R;
				byteData[index + 1] = _outline.B;
				byteData[index + 2] = _outline.G;
				byteData[index + 3] = _outline.A;
			});
		}
	}
}