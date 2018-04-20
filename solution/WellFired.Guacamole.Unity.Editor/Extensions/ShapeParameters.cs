using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public class ShapeParameters
	{
		private bool Equals(ShapeParameters other)
		{
			return _width == other._width && _height == other._height && _backgroundColor.Equals(other._backgroundColor) && _outlineColor.Equals(other._outlineColor) && _radius.Equals(other._radius) && _thickness.Equals(other._thickness) && _cornerMask == other._cornerMask && _outlineMask == other._outlineMask;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((ShapeParameters) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = _width;
				hashCode = (hashCode * 397) ^ _height;
				hashCode = (hashCode * 397) ^ _backgroundColor.GetHashCode();
				hashCode = (hashCode * 397) ^ _outlineColor.GetHashCode();
				hashCode = (hashCode * 397) ^ _radius.GetHashCode();
				hashCode = (hashCode * 397) ^ _thickness.GetHashCode();
				hashCode = (hashCode * 397) ^ (int) _cornerMask;
				hashCode = (hashCode * 397) ^ (int) _outlineMask;
				return hashCode;
			}
		}

		private readonly int _width;
		private readonly int _height;
		private readonly UIColor _backgroundColor;
		private readonly UIColor _outlineColor;
		private readonly double _radius;
		private readonly double _thickness;
		private readonly CornerMask _cornerMask;
		private readonly OutlineMask _outlineMask;

		private ShapeParameters(int width, int height, UIColor backgroundColor, UIColor outlineColor, double radius, double thickness, CornerMask cornerMask, OutlineMask outlineMask)
		{
			_width = width;
			_height = height;
			_backgroundColor = backgroundColor;
			_outlineColor = outlineColor;
			_radius = radius;
			_thickness = thickness;
			_cornerMask = cornerMask;
			_outlineMask = outlineMask;
		}

		public static ShapeParameters Create(int width, int height, UIColor backgroundColor, UIColor outlineColor, double radius, double thickness, CornerMask cornerMask, OutlineMask outlineMask)
		{
			return new ShapeParameters(width, height, backgroundColor, outlineColor, radius, thickness, cornerMask, outlineMask);
		}
	}
}