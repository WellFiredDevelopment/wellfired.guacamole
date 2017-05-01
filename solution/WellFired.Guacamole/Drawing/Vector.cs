using System;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Drawing
{
	public struct Vector
	{
		// ReSharper disable once InconsistentNaming
		// ReSharper disable once InconsistentNaming

		[PublicAPI]
		public static Vector Min { get; } = new Vector(0.0, 0.0);

		[PublicAPI]
		public static Vector One { get; } = new Vector(1.0, 1.0);

		[PublicAPI]
		public double X { get; set; }

		[PublicAPI]
		public double Y { get; set; }

		[PublicAPI]
		public double Length => Math.Sqrt(X*X + Y*Y);

		public Vector(double x, double y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj)
		{
			var compareTo = (Vector) obj;
			return (Math.Abs(compareTo.X - X) < 0.01f) && (Math.Abs(compareTo.Y - Y) < 0.01f);
		}

		public override int GetHashCode()
		{
			unchecked // Overflow is fine, just wrap
			{
				var hash = 17;
				// Suitable nullity checks etc, of course :)
				hash = hash*23 + X.GetHashCode();
				hash = hash*23 + Y.GetHashCode();
				return hash;
			}
		}

		public static bool operator ==(Vector a, Vector b)
		{
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
			return new Vector(a.X*b.X, a.Y*b.Y);
		}

		public static Vector operator /(Vector a, Vector b)
		{
			return new Vector(a.X/b.X, a.Y/b.Y);
		}

		public static Vector operator *(Vector a, double b)
		{
			return new Vector(a.X*b, a.Y*b);
		}

		public static Vector operator /(Vector a, double b)
		{
			return new Vector(a.X/b, a.Y/b);
		}

		[PublicAPI]
		public static double AngleBetween(Vector v1, Vector v2)
		{
			var angle = Math.Atan2(v2.Y, v2.X) - Math.Atan2(v1.Y, v1.X);
			if (angle < 0.0)
				angle += 2*Math.PI;
			return angle;
		}

		[PublicAPI]
		public void Normalize()
		{
			var length = Length;
			if (Math.Abs(length) < 0.00001)
				return;

			X /= length;
			Y /= length;
		}

		[PublicAPI]
		public static Vector Normalize(Vector toEnd)
		{
			var length = toEnd.Length;
			return Math.Abs(length) < 0.00001 ? Min : new Vector(toEnd.X/length, toEnd.Y /= length);
		}

		[PublicAPI]
		public void Negate()
		{
			this *= -1.0;
		}

		[PublicAPI]
		public static double Distance(Vector startPoint, Vector endPoint)
		{
			return (endPoint - startPoint).Length;
		}
	}
}