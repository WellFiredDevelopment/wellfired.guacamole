namespace WellFired.Guacamole
{
	public partial class UIColor
	{
		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }

		public static UIColor FromRGB(int red, int green, int blue)
		{
			return new UIColor {
				R = (float)red / 255.0f,
				G = (float)green / 255.0f,
				B = (float)blue / 255.0f,
			};
		}
	}
}