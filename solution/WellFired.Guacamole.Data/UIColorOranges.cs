using JetBrains.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public partial struct UIColor
	{
		[PublicAPI]
		public static UIColor DarkSalmon => FromRGB(233, 150, 122);

		[PublicAPI]
		public static UIColor Salmon => FromRGB(250, 128, 114);

		[PublicAPI]
		public static UIColor LightSalmon => FromRGB(255, 160, 122);

		[PublicAPI]
		public static UIColor Orange => FromRGB(255, 165, 0);

		[PublicAPI]
		public static UIColor DarkOrange => FromRGB(255, 140, 0);

		[PublicAPI]
		public static UIColor Coral => FromRGB(255, 127, 80);

		[PublicAPI]
		public static UIColor LightCoral => FromRGB(240, 128, 128);

		[PublicAPI]
		public static UIColor Tomato => FromRGB(255, 99, 71);

		[PublicAPI]
		public static UIColor OrangeRed => FromRGB(255, 69, 0);

		[PublicAPI]
		public static UIColor Red => FromRGB(255, 0, 0);
	}
}