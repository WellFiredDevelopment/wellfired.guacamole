using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public struct UILocation
	{
		public int X { get; set; }
		public int Y { get; set; }

		[PublicAPI]
		public static UILocation Min { get; } = Of(0);

	    public UILocation(int x, int y) : this()
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj)
		{
			var compareTo = (UILocation) obj;
			return (compareTo.X == X) && (compareTo.Y == Y);
		}

		[PublicAPI]
		public bool Equals(UILocation other)
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

		public static bool operator ==(UILocation a, UILocation b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(UILocation a, UILocation b)
		{
			return !(a == b);
		}

	    public static UILocation Of(int x, int y)
	    {
	        return new UILocation(x, y);
	    }

	    private static UILocation Of(int xAndy)
	    {
	        return Of(xAndy, xAndy);
	    }
	}
}