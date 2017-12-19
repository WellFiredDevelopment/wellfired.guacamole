using System.Diagnostics;
using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Data
{
	// ReSharper disable once InconsistentNaming
	public struct UIRect
	{
		private float _x;
		private float _y;
		private float _width;
		private float _height;
		private UILocation _location;
		private UISize _size;

		[PublicAPI]
		public static UIRect Min { get; } = With(0.0f, 0.0f, 0.0f, 0.0f);

		[PublicAPI]
		public static UIRect Max { get; } = With(0.0f, 0.0f, float.MaxValue, float.MaxValue);

		[PublicAPI]
		public static UIRect One { get; } = With(0.0f, 0.0f, 1.0f, 1.0f);

		[PublicAPI]
		public static UIRect Zero { get; } = With(0.0f, 0.0f, 0.0f, 0.0f);

		[PublicAPI]
		public float X
		{
			get => _x;
			set
			{
				_x = value;
				_location.X = _x;
			}
		}

		[PublicAPI]
		public float Y
		{
			get => _y;
			set
			{
				_y = value;
				_location.Y = _y;
			}
		}

		[PublicAPI]
		public float Width
		{
			get => _width;
			set
			{
				_width = value;
				_size.Width = _width;
			}
		}

		[PublicAPI]
		public float Height
		{
			get => _height;
			set
			{
				_height = value;
				_size.Height = _height;
			}
		}

		[PublicAPI]
		public UILocation Location
		{
			get => _location;
			set
			{
				_location = value;
				_x = _location.X;
				_y = _location.Y;
			}
		}

		[PublicAPI]
		public UISize Size
		{
			get => _size;
			set
			{
				_size = value;
				_width = _size.Width;
				_height = _size.Height;
			}
		}

		public UIRect(float x, float y, float width, float height)
		{
			_x = x;
			_y = y;
			_width = width;
			_height = height;
			_location = UILocation.Of(x, y);
			_size = UISize.Of(width, height);
		}
		
		public override bool Equals(object obj)
		{
			var compareTo = (UIRect)obj;
			Debug.Assert(obj != null, "obj != null");
			return MathUtil.NearEqual(compareTo.X, X) && MathUtil.NearEqual(compareTo.Y, Y) && MathUtil.NearEqual(compareTo.Width, Width) && MathUtil.NearEqual(compareTo.Height, Height);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode();
		}

		public static bool operator ==(UIRect a, UIRect b)
		{
			return MathUtil.NearEqual(a.X, b.X) && MathUtil.NearEqual(a.Y, b.Y) && MathUtil.NearEqual(a.Width, b.Width) && MathUtil.NearEqual(a.Height, b.Height);
		}

		public static bool operator !=(UIRect a, UIRect b)
		{
			return !MathUtil.NearEqual(a.X, b.X) || !MathUtil.NearEqual(a.Y, b.Y) || !MathUtil.NearEqual(a.Width, b.Width) || !MathUtil.NearEqual(a.Height, b.Height);
		}

		public static UIRect operator +(UIRect rect, UIPadding padding)
		{
			return With(
			    rect.X - padding.Left,
			    rect.Y - padding.Top,
			    rect.Width + padding.Width,
				rect.Height + padding.Height);
		}

		public static UIRect operator -(UIRect rect, UIPadding padding)
		{
			return With(
				rect.X + padding.Left,
				rect.Y + padding.Top,
				rect.Width - padding.Width,
				rect.Height - padding.Height);
		}

		public override string ToString()
		{
			return $"x: {X}, y: {Y}, width: {Width}, height: {Height}";
		}

	    public static UIRect With(float x, float y, float width, float height)
	    {
	        return new UIRect(x, y, width, height);
	    }

	    public static UIRect With(float width, float height)
	    {
	        return new UIRect(0, 0, width, height);
	    }

	    public static UIRect From(UISize size)
	    {
	        return With(size.Width, size.Height);
	    }
	}
}