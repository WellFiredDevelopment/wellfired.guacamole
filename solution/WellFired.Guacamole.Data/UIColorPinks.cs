using JetBrains.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public partial struct UIColor
	{
		[PublicAPI]
		public static UIColor HotPink => FromRGB(255, 105, 180);

		[PublicAPI]
		public static UIColor DeepPink => FromRGB(255, 20, 147);

		[PublicAPI]
		public static UIColor Pink => FromRGB(255, 192, 203);

		[PublicAPI]
		public static UIColor LightPink => FromRGB(255, 182, 193);

		[PublicAPI]
		public static UIColor PaleVioletRed => FromRGB(219, 112, 147);

		[PublicAPI]
		public static UIColor Maroon => FromRGB(176, 48, 96);

		[PublicAPI]
		public static UIColor MediumVioletRed => FromRGB(199, 21, 133);

		[PublicAPI]
		public static UIColor VioletRed => FromRGB(208, 32, 144);

		[PublicAPI]
		public static UIColor Violet => FromRGB(238, 130, 238);

		[PublicAPI]
		public static UIColor Plum => FromRGB(221, 160, 221);

		[PublicAPI]
		public static UIColor Orchid => FromRGB(218, 112, 214);

		[PublicAPI]
		public static UIColor MediumOrchid => FromRGB(186, 85, 211);

		[PublicAPI]
		public static UIColor DarkOrchid => FromRGB(153, 50, 204);

		[PublicAPI]
		public static UIColor DarkViolet => FromRGB(148, 0, 211);

		[PublicAPI]
		public static UIColor BlueViolet => FromRGB(138, 43, 226);

		[PublicAPI]
		public static UIColor Purple => FromRGB(160, 32, 240);

		[PublicAPI]
		public static UIColor MediumPurple => FromRGB(147, 112, 219);

		[PublicAPI]
		public static UIColor Thistle => FromRGB(216, 191, 216);
	}
}