using System;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
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
			var compareTo = (UIColor) obj;
			return (Math.Abs(compareTo.R - R) < 0.01f) && (Math.Abs(compareTo.G - G) < 0.01f) &&
			       (Math.Abs(compareTo.B - B) < 0.01f) && (Math.Abs(compareTo.A - A) < 0.01f);
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
				var hashCode = R.GetHashCode();
				hashCode = (hashCode*397) ^ G.GetHashCode();
				hashCode = (hashCode*397) ^ B.GetHashCode();
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

		public override string ToString()
		{
			return $"R: {R} G: {G} B: {B}";
		}
	}
}