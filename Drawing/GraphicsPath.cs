using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WellFired.Guacamole.Drawing.Shapes;

namespace WellFired.Guacamole.Drawing
{
	public class GraphicsPath
	{
		private readonly List<IShape> _shapes = new List<IShape>();

		public IEnumerable<Vector> Path
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

		public void Close()
		{
			for(var shapeIndex = _shapes.Count - 1; shapeIndex >= 0; shapeIndex--)
			{
				var shape = _shapes[shapeIndex];
				if(shape is Line)
					continue;

				var prevShape = shapeIndex < 1 ? _shapes.Last() : _shapes[shapeIndex - 1];
				
				_shapes.Insert(shapeIndex, new Line {
					StartPoint = shape.LastPoint(),
					EndPoint = prevShape.FirstPoint()
				});
			}
		}

		public UIColor[] Draw(int width, int height, UIColor color)
		{
			var pixelData = new UIColor[width * height];
			for (var i = 0; i < pixelData.Length; i++)
				pixelData[i] = UIColor.Clear;

			_shapes.ForEach(shape => shape.Calculate());

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

				pixelData[width * (height - y - 1) + x] = color;
			}

			Action<Pixel> recursiveFlood = pixel => {
				if (pixel.Color == color)
					return;
			};
			
			// Perform a floodfill
			var initialPiece = new Pixel((int)(width * 0.5), (int)(height * 0.5));
			recursiveFlood(initialPiece);

			return pixelData;
		}
	}
}