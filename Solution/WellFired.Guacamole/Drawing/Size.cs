using System;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Drawing
{
	public struct Size
	{
		private double _width;
		private double _height;

	    // ReSharper disable once InconsistentNaming
		private static readonly Size _min = new Size(0.0, 0.0);
	    // ReSharper disable once InconsistentNaming
		private static readonly Size _one = new Size(1.0, 1.0);

        [PublicAPI] public static Size Min { get { return _min; } }
        [PublicAPI] public static Size One { get { return _one; } }

        [PublicAPI]
        public double Width
		{
			get { return _width; }
			set
			{
				_width = value;
			}
		}

        [PublicAPI]
        public double Height
		{
			get { return _height; }
			set
			{
				_height = value;
			}
		}

	    private Size(double width, double height)
		{
			_width = width;
			_height = height;
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