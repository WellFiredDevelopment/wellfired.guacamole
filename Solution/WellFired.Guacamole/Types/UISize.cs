using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
	public struct UISize
	{
		public int Width { get; set; }
		public int Height { get; set; }
	
	    // ReSharper disable once InconsistentNaming
		private static readonly UISize _min = new UISize (0, 0);
	    // ReSharper disable once InconsistentNaming
		private static readonly UISize _max = new UISize (int.MaxValue, int.MaxValue);
	    // ReSharper disable once InconsistentNaming
		private static readonly UISize _one = new UISize (1, 1);

        [PublicAPI]
        public static UISize Min { get { return _min; } }
        [PublicAPI]
        public static UISize Max { get { return _max; } }
        [PublicAPI]
        public static UISize One { get { return _one; } }
	
		public UISize(int width, int height) : this()
		{
			Width = width;
			Height = height;
		}
	
		public override bool Equals(object obj)
		{
			var compareTo = (UISize)obj;
			return compareTo.Width == Width && compareTo.Height == Height;
		}

		public override int GetHashCode()
		{
			return Width ^ Height;
		}

		public static bool operator==(UISize a, UISize b)
		{
			return a.Equals(b);
		}

		public static bool operator!=(UISize a, UISize b)
		{
			return !(a == b);
		}
	}
}