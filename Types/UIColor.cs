using System;

namespace WellFired.Guacamole
{
	public partial struct UIColor
	{
		public float R { get; set; }
		public float G { get; set; }
		public float B { get; set; }
		public float A { get; set; }

		public static UIColor FromRGB(int red, int green, int blue)
		{
			return new UIColor {
				R = red / 255.0f,
				G = green / 255.0f,
				B = blue / 255.0f,
				A = 255.0f
			};
		}

		private static UIColor FromRGBA(int red, int green, int blue, int alpha)
		{
			return new UIColor
			{
				R = red / 255.0f,
				G = green / 255.0f,
				B = blue / 255.0f,
				A = alpha / 255.0f
			};
		}

		public override bool Equals(object obj)
		{
			var compareTo = (UIColor)obj;
			if (compareTo.R == R && compareTo.G == G && compareTo.B == B && compareTo.A == A)
				return true;

			return false;
		}

		public override int GetHashCode()
		{
			return (int)(R * 255.0f) ^ (int)(G * 255.0f) ^ (int)(B * 255.0f) ^ (int)(A * 255.0f);
		}

		public static bool operator==(UIColor a, UIColor b)
		{
			if(Object.ReferenceEquals(a, b))
				return true;

			if(((object)a == null) || ((object)b == null))
				return false;

			return a.Equals(b);
		}

		public static bool operator!=(UIColor a, UIColor b)
		{
			return !(a == b);
		}
	}
}