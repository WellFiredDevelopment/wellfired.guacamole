using System;

namespace WellFired.Guacamole
{
	public class UISize : Object
	{
		public int Width { get; set; }
		public int Height { get; set; }
	
		public static UISize Min { get { return new UISize(0, 0); } }
		public static UISize Max { get { return new UISize(int.MaxValue, int.MaxValue); } }
		public static UISize One { get { return new UISize(1, 1); } }
	
		public UISize(int width, int height)
		{
			Width = width;
			Height = height;
		}
	
		public override bool Equals(object obj)
		{
			var compareTo = obj as UISize;
			if (compareTo.Width == Width && compareTo.Height == Height)
				return true;
	
			return false;
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