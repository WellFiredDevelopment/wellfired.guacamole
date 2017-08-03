using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public struct UISize
	{
		public int Width { get; set; }
		public int Height { get; set; }

		[PublicAPI]
		public static UISize Min { get; } = Of(0);

		[PublicAPI]
		public static UISize Max { get; } = Of(int.MaxValue, int.MaxValue);

		[PublicAPI]
		public static UISize One { get; } = Of(1);

		[PublicAPI]
		public static UISize Zero { get; } = Of(0);

		public UISize(int width, int height) : this()
		{
			Width = width;
			Height = height;
		}

		public override bool Equals(object obj)
		{
			var compareTo = (UISize) obj;
			return (compareTo.Width == Width) && (compareTo.Height == Height);
		}

		[PublicAPI]
		public bool Equals(UISize other)
		{
			return (Width == other.Width) && (Height == other.Height);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return (Width*397) ^ Height;
			}
		}
		
		public static UISize operator +(UISize a, UISize b)
		{
			return Of(a.Width + b.Width, a.Height + b.Height);
		}
		
		public static UISize operator -(UISize a, UISize b)
		{
			return Of(a.Width - b.Width, a.Height - b.Height);
		}

		public static bool operator ==(UISize a, UISize b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(UISize a, UISize b)
		{
			return !(a == b);
		}

	    public override string ToString()
	    {
	        return $"{nameof(Width)}: {Width} {nameof(Height)}: {Height}";
	    }

	    public static UISize Of(int width, int height)
	    {
	        return new UISize(width, height);
	    }

	    public static UISize Of(int size)
	    {
	        return new UISize(size, size);
	    }
	}
}