using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WellFired.Guacamole.Drawing.Shapes;
using WellFired.Guacamole.Types;

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

		public UIColor[] Draw(int width, int height, UIColor backgroundColor, UIColor outlineColor)
		{
			var image = new UIImageRaw { Data = new UIColor[width * height], Width = width, Height = height };
			for (var i = 0; i < image.Length; i++)
				image[i] = UIColor.Clear;

			_shapes.ForEach(shape => shape.Calculate());

			var localOutlineColor = outlineColor;

			if(localOutlineColor == UIColor.Clear)
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

				image[width * (height - y - 1) + x] = localOutlineColor;
			}

			var startingPixel = new Pixel {
				X = (int)(width * 0.5),
				Y = (int)(height * 0.5)
			};

			new ImageFill().Fill(image, startingPixel, backgroundColor, ImageFill.FillStyle.Linear);

			return image.Data;
		}
	}
}