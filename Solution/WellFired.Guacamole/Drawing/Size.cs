using System;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Drawing
{
	public struct Size
	{
	    [PublicAPI] public static Size Min { get; } = new Size(0.0, 0.0);
	    [PublicAPI] public static Size One { get; } = new Size(1.0, 1.0);

	    [PublicAPI]
        public double Width { get; set; }

	    [PublicAPI]
        public double Height { get; set; }

	    private Size(double width, double height)
		{
			Width = width;
			Height = height;
		}

		public override bool Equals(object obj)
		{
			var compareTo = (Vector)obj;
			return Math.Abs(compareTo.X - Width) < 0.01f && Math.Abs(compareTo.Y - Height) < 0.01f;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hash = 17;
				hash = hash * 23 + Width.GetHashCode();
				hash = hash * 23 + Height.GetHashCode();
				return hash;
			}
		}

		public static bool operator ==(Size a, Size b)
		{
		    return a.Equals(b);
		}

		public static bool operator !=(Size a, Size b)
		{
			return !(a == b);
		}
	}
}