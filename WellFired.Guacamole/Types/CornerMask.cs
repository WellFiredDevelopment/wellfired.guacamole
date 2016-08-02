using System;

namespace WellFired.Guacamole
{
	[Flags]
	public enum CornerMask
	{
		TopLeft = 1 << 0,
		TopRight = 1 << 1,
		BottomLeft = 1 << 2,
		BottomRight = 1 << 3,

		All = TopLeft | TopRight | BottomLeft | BottomRight,
		Right = TopRight | BottomRight,
		Left = TopLeft | BottomLeft,
		Top = TopLeft | TopRight,
		Bottom = BottomLeft | BottomRight
	}
}