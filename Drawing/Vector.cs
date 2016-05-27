using System;

namespace WellFired.Guacamole.Drawing
{
	public struct Vector
	{
		private double _x;
		private double _y;

		private static readonly Vector _min = new Vector(0.0, 0.0);
		private static readonly Vector _one = new Vector(1.0, 1.0);

		public static Vector Min { get { return _min; } }
		public static Vector One { get { return _one; } }

		public double X
		{
			get { return _x; }
			set
			{
				_x = value;
			}
		}

		public double Y
		{
			get { return _y; }
			set
			{
				_y = value;
			}
		}

		public double Length
		{
			get
			{
				return Math.Sqrt(X * X + Y * Y);
			}
		}

		public Vector(double x, double y)
		{
			_x = x;
			_y = y;
		}

		public override bool Equals(object obj)
		{
			var compareTo = (Vector)obj;
			return Math.Abs(compareTo.X - X) < 0.01f && Math.Abs(compareTo.Y - Y) < 0.01f;
		}

		public override int GetHashCode()
		{
			unchecked // Overflow is fine, just wrap
			{
				var hash = 17;
				// Suitable nullity checks etc, of course :)
				hash = hash * 23 + X.GetHashCode();
				hash = hash * 23 + Y.GetHashCode();
				return hash;
			}
		}

		public static bool operator ==(Vector a, Vector b)
		{
			if (Object.ReferenceEquals(a, b))
				return true;

			if (((object)a == null) || ((object)b == null))
				return false;

			return a.Equals(b);
		}

		public static bool operator !=(Vector a, Vector b)
		{
			return !(a == b);
		}

		public static Vector operator -(Vector a, Vector b)
		{
			return new Vector(a.X - b.X, a.Y - b.Y);
		}

		public static Vector operator +(Vector a, Vector b)
		{
			return new Vector(a.X + b.X, a.Y + b.Y);
		}

		public static Vector operator *(Vector a, Vector b)
		{
			return new Vector(a.X * b.X, a.Y * b.Y);
		}

		public static Vector operator /(Vector a, Vector b)
		{
			return new Vector(a.X / b.X, a.Y / b.Y);
		}

		public static Vector operator *(Vector a, double b)
		{
			return new Vector(a.X * b, a.Y * b);
		}

		public static Vector operator /(Vector a, double b)
		{
			return new Vector(a.X / b, a.Y / b);
		}

		public static double AngleBetween(Vector v1, Vector v2)
		{
			var angle = Math.Atan2(v2._y, v2._x) - Math.Atan2(v1._y, v1._x);
			if (angle < 0.0)
				angle += 2 * Math.PI;
			return angle;
		}

		public void Normalize()
		{
			if (Math.Abs(Length) < 0.00001)
				return;

			X /= Length;
			Y /= Length;
		}

		public void Negate()
		{
			this *= -1.0f;
		}
	}
}