using System.Collections.Generic;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Drawing.Shapes;

namespace WellFired.Guacamole.Drawing
{
	public class GraphicsPath
	{
		private readonly List<IRasterizableShape> _shapes = new List<IRasterizableShape>();
		private readonly double _thickness;

		public GraphicsPath(double thickness)
		{
			_thickness = thickness;
		}

		public void AddLine(Vector startPoint, Vector endPoint)
		{
			AddRasterizableShapeShape(new Line(startPoint, endPoint, _thickness));
		}

		public void AddCircleQuarter(QuarterCircle.Quarter quarter, Vector center, double radius)
		{
			AddRasterizableShapeShape(new QuarterCircle(quarter, center, radius, _thickness));
		}

		public void AddCircle(Vector center, double radius)
		{
			AddRasterizableShapeShape(new Circle(center, radius, _thickness));
		}

		public void AddRectDefinedEllipse(Rect rect)
		{
			AddRasterizableShapeShape(new RectDefinedEllipse(rect, _thickness));
		}

		public void AddRasterizableShapeShape(IRasterizableShape shape)
		{
			_shapes.Add(shape);
		}

		public UIColor[] Draw(int width, int height, UIColor backgroundColor, UIColor outlineColor)
		{
			var image = new UIImageRaw {Data = new UIColor[width * height], Width = width, Height = height};
			for (var i = 0; i < image.Length; i++)
				image[i] = UIColor.Clear;

			if (backgroundColor == UIColor.Clear && outlineColor == UIColor.Clear)
				return image.Data;

			_shapes.ForEach(shape => shape.RasterizeWithWidthAndAA(image, outlineColor));

			var startingPixel = new Pixel
			{
				X = (int) (width*0.5),
				Y = (int) (height*0.5)
			};

			new ImageFill().Fill(image, startingPixel, backgroundColor, ImageFill.FillStyle.Linear);
			
			return image.Data;
		}
	}
}