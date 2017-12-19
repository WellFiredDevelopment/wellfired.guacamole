using System;
using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Drawing
{
	public struct Rect
	{
		[PublicAPI]
		public static Rect Min { get; } = new Rect(0, 0, 0, 0);

		[PublicAPI]
		public static Rect Max { get; } = new Rect(0, 0, int.MaxValue, int.MaxValue);

		[PublicAPI]
		public static Rect One { get; } = new Rect(0, 0, 1, 1);

		[PublicAPI]
		public double X { get; set; }

		[PublicAPI]
		public double Y { get; set; }

		[PublicAPI]
		public double Width { get; set; }

		[PublicAPI]
		public double Height { get; set; }

		[PublicAPI]
		public Vector Center => new Vector(X + Width*0.5f, Y + Height*0.5f);

		public Rect(double x, double y, double width, double height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public Rect(double x, double y, Size size)
		{
			X = x;
			Y = y;
			Width = size.Width;
			Height = size.Height;
		}

		public override bool Equals(object obj)
		{
			var compareTo = (Rect) obj;
			return
				(Math.Abs(compareTo.X - X) < 0.0001) &&
				(Math.Abs(compareTo.Y - Y) < 0.0001) &&
				(Math.Abs(compareTo.Width - Width) < 0.0001) &&
				(Math.Abs(compareTo.Height - Height) < 0.0001);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hash = 17;
				hash = hash*23 + X.GetHashCode();
				hash = hash*23 + Y.GetHashCode();
				hash = hash*23 + Width.GetHashCode();
				hash = hash*23 + Height.GetHashCode();
				return hash;
			}
		}

		public static bool operator ==(Rect a, Rect b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Rect a, Rect b)
		{
			return !(a == b);
		}
	}
}