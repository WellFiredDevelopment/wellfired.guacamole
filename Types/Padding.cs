using System;

namespace WellFired.Guacamole
{
	public struct Padding
	{
		public int Left { get; set; }
		public int Top { get; set; }
		public int Right { get; set; }
		public int Bottom { get; set; }
		public int Width { get { return Left + Right; } }
		public int Height { get { return Top + Bottom; } }

		public Padding(int equalPadding)
		{
			Left = equalPadding;
			Top = equalPadding;
			Right = equalPadding;
			Bottom = equalPadding;
		}

		public Padding(int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		static public implicit operator Padding(int value)
		{
			return new Padding(value);
		}

		public override bool Equals(object obj)
		{
			var compareTo = (Padding)obj;
			if (compareTo.Left == Left && compareTo.Top == Top && compareTo.Right == Right && compareTo.Bottom == Bottom)
				return true;

			return false;
		}

		public override int GetHashCode()
		{
			return ((Left * 300) ^ (Top * 300) ^ (Right * 300) ^ (Bottom * 300));
		}

		public static bool operator==(Padding a, Padding b)
		{
			if(Object.ReferenceEquals(a, b))
				return true;

			if(((object)a == null) || ((object)b == null))
				return false;

			return a.Equals(b);
		}

		public static bool operator!=(Padding a, Padding b)
		{
			return !(a == b);
		}
	}
}