using System.Diagnostics;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Types
{
	// ReSharper disable once InconsistentNaming
	public struct UIRect
	{
		private int _x;
		private int _y;
		private int _width;
		private int _height;
		private UILocation _location;
		private UISize _size;

		[PublicAPI]
		public static UIRect Min { get; } = With(0, 0, 0, 0);

		[PublicAPI]
		public static UIRect Max { get; } = With(0, 0, int.MaxValue, int.MaxValue);

		[PublicAPI]
		public static UIRect One { get; } = With(0, 0, 1, 1);

		[PublicAPI]
		public static UIRect Zero { get; } = With(0, 0, 0, 0);

		[PublicAPI]
		public int X
		{
			get { return _x; }
			set
			{
				_x = value;
				_location.X = _x;
			}
		}

		[PublicAPI]
		public int Y
		{
			get { return _y; }
			set
			{
				_y = value;
				_location.Y = _y;
			}
		}

		[PublicAPI]
		public int Width
		{
			get { return _width; }
			set
			{
				_width = value;
				_size.Width = _width;
			}
		}

		[PublicAPI]
		public int Height
		{
			get { return _height; }
			set
			{
				_height = value;
				_size.Height = _height;
			}
		}

		[PublicAPI]
		public UILocation Location
		{
			get { return _location; }
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
			get { return _size; }
			set
			{
				_size = value;
				_width = _size.Width;
				_height = _size.Height;
			}
		}

		public UIRect(int x, int y, int width, int height)
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
			Debug.Assert(obj != null, "obj != null");
			var compareTo = (UIRect)obj;
			return (compareTo.X == X) && (compareTo.Y == Y) && (compareTo.Width == Width) && (compareTo.Height == Height);
		}

		public override int GetHashCode()
		{
			return X ^ Y ^ Width ^ Height;
		}

		public static bool operator ==(UIRect a, UIRect b)
		{
			return (a.X == b.X) && (a.Y == b.Y) && (a.Width == b.Width) && (a.Height == b.Height);
		}

		public static bool operator !=(UIRect a, UIRect b)
		{
			return a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
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

	    public static UIRect With(int x, int y, int width, int height)
	    {
	        return new UIRect(x, y, width, height);
	    }

	    public static UIRect With(int width, int height)
	    {
	        return new UIRect(0, 0, width, height);
	    }

	    public static UIRect From(UISize size)
	    {
	        return With(size.Width, size.Height);
	    }
	}
}