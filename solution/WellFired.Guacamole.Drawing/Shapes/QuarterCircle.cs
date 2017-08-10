namespace WellFired.Guacamole.Drawing.Shapes
{
	public class QuarterCircle : IRasterizableShape
	{
		public enum Quarter
		{
			TopRight,
			BottomRight,
			BottomLeft,
			TopLeft
		}
		
		private readonly IRasterizableShape _rasterizer;

		public QuarterCircle(Quarter quarter, Vector center, double radius, double thickness, ByteColor background, ByteColor outline)
		{
			_rasterizer = new Shapes.Quarter(quarter, center, radius, thickness, background, outline);
		}

		public void Rasterize(byte[] byteData, int width, int height)
		{
			_rasterizer.Rasterize(byteData, width, height);
		}
	}
}