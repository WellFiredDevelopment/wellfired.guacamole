using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public struct UILocation
	{
		public float X { get; set; }
		public float Y { get; set; }

		[PublicAPI]
		public static UILocation Min { get; } = Of(0);

		public UILocation(float x, float y) : this()
		{
			X = x;
			Y = y;
		}
		
		public override bool Equals(object obj)
		{
			var compareTo = (UILocation) obj;
			return MathUtil.NearEqual(compareTo.X, X) && MathUtil.NearEqual(compareTo.Y, Y);
		}

		[PublicAPI]
		public bool Equals(UILocation other)
		{
			return MathUtil.NearEqual(X, other.X) && MathUtil.NearEqual(Y, other.Y);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				// ReSharper disable once NonReadonlyMemberInGetHashCode
				return (X.GetHashCode() * 397) ^ 
				       // ReSharper disable once NonReadonlyMemberInGetHashCode
				       Y.GetHashCode();
			}
		}

		public static bool operator ==(UILocation a, UILocation b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(UILocation a, UILocation b)
		{
			return !(a == b);
		}

		public static UILocation Of(float x, float y)
		{
			return new UILocation(x, y);
		}

		private static UILocation Of(float xAndy)
		{
			return Of(xAndy, xAndy);
		}
	}
}