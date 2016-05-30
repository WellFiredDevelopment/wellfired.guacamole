using System;

namespace WellFired.Guacamole.Drawing
{
	public struct Pixel
	{
		public int X;
		public int Y;
		public UIColor Color;

		public override bool Equals(object obj)
		{
			var compareTo = (Pixel)obj;
			return compareTo.X == X && compareTo.Y == Y && compareTo.Color == Color;
		}

		public override int GetHashCode()
		{
			return X ^ Y ^ Color.GetHashCode();
		}

		public static bool operator ==(Pixel a, Pixel b)
		{
			if (Object.ReferenceEquals(a, b))
				return true;

			if (((object)a == null) || ((object)b == null))
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Pixel a, Pixel b)
		{
			return !(a == b);
		}
	}
}