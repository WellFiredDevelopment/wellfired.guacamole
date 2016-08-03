using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
    // ReSharper disable once InconsistentNaming
	public struct UIPadding
    {
        [PublicAPI] public int Left { get; private set; }
        [PublicAPI] public int Top { get; private set; }
        [PublicAPI] public int Right { get; private set; }
        [PublicAPI] public int Bottom { get; private set; }
        [PublicAPI] public int Width { get { return Left + Right; } }
        [PublicAPI] public int Height { get { return Top + Bottom; } }

		public UIPadding(int equalPadding) : this()
		{
			Left = equalPadding;
			Top = equalPadding;
			Right = equalPadding;
			Bottom = equalPadding;
		}

		public UIPadding(int left, int top, int right, int bottom) : this()
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		public static implicit operator UIPadding(int value)
		{
			return new UIPadding(value);
		}

		public override bool Equals(object obj)
		{
			var compareTo = (UIPadding)obj;
			return compareTo.Left == Left && compareTo.Top == Top && compareTo.Right == Right && compareTo.Bottom == Bottom;
		}

		public override int GetHashCode()
		{
			return ((Left * 300) ^ (Top * 300) ^ (Right * 300) ^ (Bottom * 300));
		}

		public static bool operator==(UIPadding a, UIPadding b)
		{
			return a.Equals(b);
		}

		public static bool operator!=(UIPadding a, UIPadding b)
		{
			return !(a == b);
		}
	}
}