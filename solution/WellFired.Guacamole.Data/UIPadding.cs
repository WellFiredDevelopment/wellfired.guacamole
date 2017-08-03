using System.ComponentModel;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.Data.Converters;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	[TypeConverter(typeof(UIPaddingConverter))]
	public struct UIPadding
	{
		[PublicAPI]
		public int Left { get; private set; }

		[PublicAPI]
		public int Top { get; private set; }

		[PublicAPI]
		public int Right { get; private set; }

		[PublicAPI]
		public int Bottom { get; private set; }

		[PublicAPI]
		public int Width => Left + Right;

		[PublicAPI]
		public int Height => Top + Bottom;

	    public static UIPadding Zero => Of(0);
	    public static UIPadding One => Of(1);

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
			return Of(value);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
				return false;

			var paddingValue = new UIPadding();
			if (obj is int)
				paddingValue = (int) obj;
			else if (obj is UIPadding)
				paddingValue = (UIPadding) obj;

			return Equals(paddingValue);
		}

		[PublicAPI]
		public bool Equals(UIPadding other)
		{
			var result = (Left == other.Left) && (Top == other.Top) && (Right == other.Right) && (Bottom == other.Bottom);
			return result;
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

		public static bool operator ==(UIPadding a, UIPadding b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(UIPadding a, UIPadding b)
		{
			return !(a == b);
		}

	    public static UIPadding Of(int i)
	    {
	        return new UIPadding(i);
	    }

		public static UIPadding With(int left, int top, int right, int bottom)
		{
			return new UIPadding(left, top, right, bottom);
		}
	}
}