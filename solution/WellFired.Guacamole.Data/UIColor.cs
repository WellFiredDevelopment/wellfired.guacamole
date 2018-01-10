using System;
using JetBrains.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public partial struct UIColor
	{

		[PublicAPI]
		public float R { get; set; }

		[PublicAPI]
		public float G { get; set; }

		[PublicAPI]
		public float B { get; set; }

		[PublicAPI]
		public float A { get; set; }

		// ReSharper disable once InconsistentNaming
		[PublicAPI]
		public static UIColor FromRGB(int red, int green, int blue)
		{
			return new UIColor
			{
				R = red/255.0f,
				G = green/255.0f,
				B = blue/255.0f,
				A = 1.0f
			};
		}

		// ReSharper disable once InconsistentNaming
		[PublicAPI]
		public static UIColor FromRGBA(int red, int green, int blue, int alpha)
		{
			return new UIColor
			{
				R = red/255.0f,
				G = green/255.0f,
				B = blue/255.0f,
				A = alpha/255.0f
			};
		}

		public override bool Equals(object obj)
		{
			// ReSharper disable once PossibleNullReferenceException
			var compareTo = (UIColor) obj;
			return Math.Abs(compareTo.R - R) < 0.01f && Math.Abs(compareTo.G - G) < 0.01f &&
			       Math.Abs(compareTo.B - B) < 0.01f && Math.Abs(compareTo.A - A) < 0.01f;
		}

		[PublicAPI]
		public bool Equals(UIColor other)
		{
			return R.Equals(other.R) && G.Equals(other.G) && B.Equals(other.B) && A.Equals(other.A);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				// ReSharper disable once NonReadonlyMemberInGetHashCode
				var hashCode = R.GetHashCode();
				// ReSharper disable once NonReadonlyMemberInGetHashCode
				hashCode = (hashCode*397) ^ G.GetHashCode();
				// ReSharper disable once NonReadonlyMemberInGetHashCode
				hashCode = (hashCode*397) ^ B.GetHashCode();
				// ReSharper disable once NonReadonlyMemberInGetHashCode
				hashCode = (hashCode*397) ^ A.GetHashCode();
				return hashCode;
			}
		}

		public static bool operator ==(UIColor a, UIColor b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(UIColor a, UIColor b)
		{
			return !(a == b);
		}

		public static UIColor operator *(UIColor a, float b)
		{
			return new UIColor
			{
				R = a.R * b,
				G = a.G * b,
				B = a.B * b,
				A = a.A * b
			};
		}

		public static UIColor operator +(UIColor a, UIColor b)
		{
			return new UIColor
			{
				R = a.R + b.R,
				G = a.G + b.G,
				B = a.B + b.B,
				A = a.A + b.A
			};
		}

		public override string ToString()
		{
			return $"R: {R} G: {G} B: {B}";
		}
	}
}