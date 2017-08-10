using System;

namespace WellFired.Guacamole.Drawing
{
	public struct Pixel : IEquatable<Pixel>
	{
		public int X { get; }
		public int Y { get; }

		public Pixel(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj)
		{
			var compareTo = (Pixel) obj;
			return (compareTo.X == X) && (compareTo.Y == Y);
		}

		public bool Equals(Pixel other)
		{
			return (X == other.X) && (Y == other.Y);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (X*397) ^ Y;
			}
		}

		public static bool operator ==(Pixel a, Pixel b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Pixel a, Pixel b)
		{
			return !(a == b);
		}
	}
}