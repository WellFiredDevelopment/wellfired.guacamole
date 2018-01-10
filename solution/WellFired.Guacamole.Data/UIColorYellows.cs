using JetBrains.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public partial struct UIColor
	{
		[PublicAPI]
		public static UIColor LightGoldenrodYellow => FromRGB(250, 250, 210);

		[PublicAPI]
		public static UIColor LightYellow => FromRGB(255, 255, 224);

		[PublicAPI]
		public static UIColor Yellow => FromRGB(255, 255, 0);

		[PublicAPI]
		public static UIColor Gold => FromRGB(255, 215, 0);

		[PublicAPI]
		public static UIColor LightGoldenrod => FromRGB(238, 221, 130);

		[PublicAPI]
		public static UIColor Goldenrod => FromRGB(218, 165, 32);

		[PublicAPI]
		public static UIColor DarkGoldenrod => FromRGB(184, 134, 11);
	}
}