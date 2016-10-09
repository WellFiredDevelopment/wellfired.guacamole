using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
	// ReSharper disable once InconsistentNaming
	public struct UISize
	{
		public int Width { get; set; }
		public int Height { get; set; }

		[PublicAPI]
		public static UISize Min { get; } = new UISize(0, 0);

		[PublicAPI]
		public static UISize Max { get; } = new UISize(int.MaxValue, int.MaxValue);

		[PublicAPI]
		public static UISize One { get; } = new UISize(1, 1);

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

		public static bool operator ==(UISize a, UISize b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(UISize a, UISize b)
		{
			return !(a == b);
		}
	}
}