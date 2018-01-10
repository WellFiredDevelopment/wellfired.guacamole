using JetBrains.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public partial struct UIColor
	{
		[PublicAPI]
		public static UIColor Snow => FromRGB(255, 250, 250);

		[PublicAPI]
		public static UIColor GhostWhite => FromRGB(248, 248, 255);

		[PublicAPI]
		public static UIColor WhiteSmoke => FromRGB(245, 245, 245);

		[PublicAPI]
		public static UIColor FloralWhite => FromRGB(255, 250, 240);

		[PublicAPI]
		public static UIColor White => FromRGB(255, 255, 255);

		[PublicAPI]
		public static UIColor Clear => FromRGBA(255, 255, 255, 0);
	}
}