using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public partial struct UIColor
	{
		[PublicAPI]
		public static UIColor Black => FromRGB(0, 0, 0);

		[PublicAPI]
		public static UIColor DarkSlateGrey => FromRGB(49, 79, 79);

		[PublicAPI]
		public static UIColor DimGrey => FromRGB(105, 105, 105);

		[PublicAPI]
		public static UIColor SlateGrey => FromRGB(112, 138, 144);

		[PublicAPI]
		public static UIColor LightSlateGrey => FromRGB(119, 136, 153);

		[PublicAPI]
		public static UIColor Grey => FromRGB(190, 190, 190);

		[PublicAPI]
		public static UIColor LightGrey => FromRGB(211, 211, 211);
	}
}