using WellFired.Guacamole.Data;
using WellFired.Guacamole.Drawing.Shapes;

// Disabled here since it makes passing variables to the shape constructors easier to read
// ReSharper disable ArgumentsStyleNamedExpression

namespace WellFired.Guacamole.Drawing
{
	public class GraphicsPath
	{
		private IRasterizableShape _shape;

		public void FromCircle(Vector center, double radius, double thickness, ByteColor background, ByteColor outline)
		{
			FromRasterizableShape(new Circle(
				center: center,
				radius: radius,
				background: background, 
				outline: outline,
				thickness: thickness));
		}

		public void FromDonut(Vector center, double radius, double holeRadius, ByteColor background)
		{
			FromRasterizableShape(new DonutFill(
				center: center,
				radius: radius,
				holeRadius: holeRadius,
				background: background));
		}

		public void FromRoundedCornerRect(Rect rect, double radius, double thickness, ByteColor background, ByteColor outline, CornerMask cornerMask, OutlineMask outlineMask)
		{
			FromRasterizableShape(new RoundedCornerRect(
				rect: rect,
				radius: radius,
				thickness: thickness,
				background: background,
				outline: outline,
				cornerMask: cornerMask, 
				outlineMask: outlineMask));
		}

		public void FromRect(Rect rect, double thickness, ByteColor background, ByteColor outline, OutlineMask outlineMask)
		{
			FromRasterizableShape(new SquareRect(
				rect: rect,
				thickness: thickness,
				background: background,
				outline: outline,
				outlineMask: outlineMask));
		}

		public void FromLine(Vector startPoint, Vector endPoint)
		{
			//FromRasterizableShape(new Line(startPoint, endPoint, _thickness));
		}

		public void FromCircleQuarter(QuarterCircle.Quarter quarter, Vector center, double radius, double thickness, ByteColor background, ByteColor outline)
		{
			FromRasterizableShape(new Quarter(quarter, center, radius, thickness, background, outline));
		}

		public void FromRectDefinedEllipse(Rect rect)
		{
			//FromRasterizableShape(new RectDefinedEllipse(rect));
		}

		public void FromRasterizableShape(IRasterizableShape shape)
		{
			_shape = shape;
		}

		public byte[] Draw(int width, int height)
		{
			// Bytes default to 0 so we don't need to clear this array.
			var byteData = new byte[width * height * 4];

			_shape?.Rasterize(
				byteData: byteData, 
				width: width, 
				height: height);
			
			return byteData;
		}
	}
}