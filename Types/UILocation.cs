using System;

namespace WellFired.Guacamole
{
	public class UILocation : Object
	{
		public int X { get; set; }
		public int Y { get; set; }
	
		public UILocation(int x, int y)
		{
			X = x;
			Y = y;
		}
	
		public override bool Equals(object obj)
		{
			var compareTo = obj as UILocation;
			if (compareTo.X == X && compareTo.Y == Y)
				return true;
	
			return false;
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