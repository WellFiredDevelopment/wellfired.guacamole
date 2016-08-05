using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
	public struct UILocation
	{
		public int X { get; set; }
		public int Y { get; set; }

        // ReSharper disable once InconsistentNaming
        private static readonly UILocation _min = new UILocation(0, 0);

        [PublicAPI]
        public static UILocation Min { get { return _min; } }
	
		public UILocation(int x, int y) : this()
		{
			X = x;
			Y = y;
		}

	    public override bool Equals(object obj)
		{
			var compareTo = (UILocation)obj;
			return compareTo.X == X && compareTo.Y == Y;
		}

		public override int GetHashCode()
		{
			return X ^ Y;
		}

		public static bool operator==(UILocation a, UILocation b)
		{
			return a.Equals(b);
		}

		public static bool operator!=(UILocation a, UILocation b)
		{
			return !(a == b);
		}
	}
}