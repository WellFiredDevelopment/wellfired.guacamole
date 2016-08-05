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
        [PublicAPI] public int Width => Left + Right;
	    [PublicAPI] public int Height => Top + Bottom;

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

        [PublicAPI]
        public bool Equals(UIPadding other)
	    {
	        return Left == other.Left && Top == other.Top && Right == other.Right && Bottom == other.Bottom;
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            var hashCode = Left;
	            hashCode = (hashCode*397) ^ Top;
	            hashCode = (hashCode*397) ^ Right;
	            hashCode = (hashCode*397) ^ Bottom;
	            return hashCode;
	        }
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