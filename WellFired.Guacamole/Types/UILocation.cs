using System;

namespace WellFired.Guacamole
{
    // ReSharper disable once InconsistentNaming
	public struct UILocation
	{
		public int X { get; set; }
		public int Y { get; set; }

	    // ReSharper disable once InconsistentNaming
	    private static readonly UILocation min;

		public static UILocation Min { get { return min; } }
	
		public UILocation(int x, int y) : this()
		{
			X = x;
			Y = y;
		}

	    public override bool Equals(object obj)
		{
			var compareTo = (UILocation)obj;
			if (compareTo.X == X && compareTo.Y == Y)
				return true;
	
			return false;
		}

		public override int GetHashCode()
		{
			return X ^ Y;
		}

		public static bool operator==(UILocation a, UILocation b)
		{
			if(Object.ReferenceEquals(a, b))
				return true;

			if(((object)a == null) || ((object)b == null))
				return false;

			return a.Equals(b);
		}

		public static bool operator!=(UILocation a, UILocation b)
		{
			return !(a == b);
		}
	}
}