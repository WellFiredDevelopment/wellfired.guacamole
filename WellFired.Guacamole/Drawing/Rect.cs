using System;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Drawing
{
	public struct Rect
	{
		private double _x;
		private double _y;
		private double _width;
		private double _height;

	    // ReSharper disable once InconsistentNaming
		private static readonly Rect _min = new Rect(0, 0, 0, 0);
	    // ReSharper disable once InconsistentNaming
		private static readonly Rect _max = new Rect(0, 0, int.MaxValue, int.MaxValue);
	    // ReSharper disable once InconsistentNaming
		private static readonly Rect _one = new Rect(0, 0, 1, 1);

        [PublicAPI] public static Rect Min { get { return _min; } }
        [PublicAPI] public static Rect Max { get { return _max; } }
        [PublicAPI] public static Rect One { get { return _one; } }

        [PublicAPI]
        public double X
		{
			get { return _x; }
			set
			{
				_x = value;
			}
		}

        [PublicAPI]
        public double Y
		{
			get { return _y; }
			set
			{
				_y = value;
			}
		}

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

        [PublicAPI]
        public Vector Center
		{
			get
			{
				return new Vector(X + Width * 0.5f, Y + Height * 0.5f);
			}
		}

		public Rect(double x, double y, double width, double height)
		{
			_x = x;
			_y = y;
			_width = width;
			_height = height;
		}

		public Rect(double x, double y, Size size)
		{
			_x = x;
			_y = y;
			_width = size.Width;
			_height = size.Height;
		}

		public override bool Equals(object obj)
		{
			var compareTo = (Rect)obj;
			return 
				Math.Abs(compareTo.X - X) < 0.0001 && 
				Math.Abs(compareTo.Y - Y) < 0.0001 && 
				Math.Abs(compareTo.Width - Width) < 0.0001 && 
				Math.Abs(compareTo.Height - Height) < 0.0001;
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hash = 17;
				hash = hash * 23 + X.GetHashCode();
				hash = hash * 23 + Y.GetHashCode();
				hash = hash * 23 + Width.GetHashCode();
				hash = hash * 23 + Height.GetHashCode();
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