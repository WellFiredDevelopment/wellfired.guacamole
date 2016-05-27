using System;

namespace WellFired.Guacamole.Drawing
{
	public struct Size
	{
		private double _width;
		private double _height;

		private static readonly Size _min = new Size(0.0, 0.0);
		private static readonly Size _one = new Size(1.0, 1.0);

		public static Size Min { get { return _min; } }
		public static Size One { get { return _one; } }

		public double Width
		{
			get { return _width; }
			set
			{
				_width = value;
			}
		}

		public double Height
		{
			get { return _height; }
			set
			{
				_height = value;
			}
		}

		public Size(double width, double height)
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
			unchecked // Overflow is fine, just wrap
			{
				int hash = 17;
				// Suitable nullity checks etc, of course :)
				hash = hash * 23 + Width.GetHashCode();
				hash = hash * 23 + Height.GetHashCode();
				return hash;
			}
		}

		public static bool operator ==(Size a, Size b)
		{
			if (Object.ReferenceEquals(a, b))
				return true;

			if (((object)a == null) || ((object)b == null))
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Size a, Size b)
		{
			return !(a == b);
		}
	}
}