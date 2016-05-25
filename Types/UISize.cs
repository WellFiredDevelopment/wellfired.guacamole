using System;

namespace WellFired.Guacamole
{
	public struct UISize
	{
		public int Width { get; set; }
		public int Height { get; set; }
	
		private static UISize min = new UISize (0, 0);
		private static UISize max = new UISize (int.MaxValue, int.MaxValue);
		private static UISize one = new UISize (1, 1);

		public static UISize Min { get { return min; } }
		public static UISize Max { get { return max; } }
		public static UISize One { get { return one; } }
	
		public UISize(int width, int height)
		{
			Width = width;
			Height = height;
		}
	
		public override bool Equals(object obj)
		{
			var compareTo = (UISize)obj;
			if (compareTo.Width == Width && compareTo.Height == Height)
				return true;
	
			return false;
		}

		public override int GetHashCode()
		{
			return Width ^ Height;
		}

		public static bool operator==(UISize a, UISize b)
		{
			if(Object.ReferenceEquals(a, b))
				return true;
			
			if(((object)a == null) || ((object)b == null))
				return false;
			
			return a.Equals(b);
		}

		public static bool operator!=(UISize a, UISize b)
		{
			return !(a == b);
		}
	}
}