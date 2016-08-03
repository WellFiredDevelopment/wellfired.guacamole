using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Drawing.Shapes;

namespace WellFired.Guacamole.Drawing
{
	public class GraphicsPath
	{
		private readonly List<IShape> _shapes = new List<IShape>();

	    private IEnumerable<Vector> Path
		{
			get { return _shapes.SelectMany(shape => shape.Path); }
		}

		public void AddArc(Rect arcRect, double startAngle, double sweepAngle)
		{
			_shapes.Add(new Arc {
				ArcRect = arcRect,
				StartAngle = startAngle,
				SweepAngle = sweepAngle
			});
		}

		public void AddLine(Vector startPoint, Vector endPoint)
		{
			_shapes.Add(new Line { StartPoint = startPoint, EndPoint = endPoint });
		}

		public Types.UIColor[] Draw(int width, int height, Types.UIColor backgroundColor, Types.UIColor outlineColor)
		{
			var pixelData = new Types.UIColor[width * height];
			for (var i = 0; i < pixelData.Length; i++)
				pixelData[i] = Types.UIColor.Clear;

			_shapes.ForEach(shape => shape.Calculate());

			var localOutlineColor = outlineColor;

			if(localOutlineColor == Types.UIColor.Clear)
				localOutlineColor = backgroundColor;

			foreach(var point in Path)
			{
				var x = (int)point.X;
				var y = (int)point.Y;

				if (x < 0)
					continue;
				if (y < 0)
					continue;

				if (x == width)
					x -= 1;
				if (y == height)
					y -= 1;

				if (x >= width)
					continue;
				if (y >= height)
					continue;

				pixelData[width * (height - y - 1) + x] = localOutlineColor;
			}

			Action<Pixel, Types.UIColor, Types.UIColor> recursiveFlood = delegate {};
			recursiveFlood = (pixel, targetColor, replacementColor) => {
				if (targetColor == backgroundColor)
					return;

				var pixelColor = pixelData[width * (height - pixel.Y - 1) + pixel.X];
				if (pixelColor != targetColor)
					return;

				if(pixel.X < 0 || pixel.Y < 0 || pixel.X >= width || pixel.Y >= height)
					return;
				
				pixelData[width * (height - pixel.Y - 1) + pixel.X] = replacementColor;

				var northPixel = new Pixel
				{
					X = pixel.X,
					Y = pixel.Y + 1
				};
				var eastPixel = new Pixel
				{
					X = pixel.X + 1,
					Y = pixel.Y
				};
				var southPixel = new Pixel
				{
					X = pixel.X,
					Y = pixel.Y - 1
				};
				var westPixel = new Pixel
				{
					X = pixel.X - 1,
					Y = pixel.Y
				};
				
			    // ReSharper disable once AccessToModifiedClosure
				recursiveFlood(northPixel, targetColor, replacementColor);
			    // ReSharper disable once AccessToModifiedClosure
				recursiveFlood(eastPixel, targetColor, replacementColor);
			    // ReSharper disable once AccessToModifiedClosure
				recursiveFlood(southPixel, targetColor, replacementColor);
			    // ReSharper disable once AccessToModifiedClosure
				recursiveFlood(westPixel, targetColor, replacementColor);
			};
			
			// Perform a floodfill
			var initialPiece = new Pixel
			{
				X = (int) (width*0.5),
				Y = (int) (height*0.5)
			};

			recursiveFlood(initialPiece, Types.UIColor.Clear, backgroundColor);

			return pixelData;
		}
	}
}