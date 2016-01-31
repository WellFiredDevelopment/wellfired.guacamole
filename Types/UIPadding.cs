using System;

namespace WellFired.Guacamole
{
	public struct UIPadding
	{
		public int Left { get; set; }
		public int Top { get; set; }
		public int Right { get; set; }
		public int Bottom { get; set; }
		public int Width { get { return Left + Right; } }
		public int Height { get { return Top + Bottom; } }

		public UIPadding(int equalPadding)
		{
			Left = equalPadding;
			Top = equalPadding;
			Right = equalPadding;
			Bottom = equalPadding;
		}

		public UIPadding(int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		static public implicit operator UIPadding(int value)
		{
			return new UIPadding(value);
		}

		public override bool Equals(object obj)
		{
			var compareTo = (UIPadding)obj;
			if (compareTo.Left == Left && compareTo.Top == Top && compareTo.Right == Right && compareTo.Bottom == Bottom)
				return true;

			return false;
		}

		public override int GetHashCode()
		{
			return ((Left * 300) ^ (Top * 300) ^ (Right * 300) ^ (Bottom * 300));
		}

		public static bool operator==(UIPadding a, UIPadding b)
		{
			if(Object.ReferenceEquals(a, b))
				return true;

			if(((object)a == null) || ((object)b == null))
				return false;

			return a.Equals(b);
		}

		public static bool operator!=(UIPadding a, UIPadding b)
		{
			return !(a == b);
		}
	}
}