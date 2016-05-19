using System;

namespace WellFired.Guacamole
{
	public struct UIRect
	{
		private int x;
		private int y;
		private int width;
		private int height;
		public UILocation location;
		public UISize size;

		private static UIRect min = new UIRect (0, 0, 0, 0);
		private static UIRect max = new UIRect (0, 0, int.MaxValue, int.MaxValue);
		private static UIRect one = new UIRect (0, 0, 1, 1);

		public static UIRect Min { get { return min; } }
		public static UIRect Max { get { return max; } }
		public static UIRect One { get { return one; } }

		public int X 
		{
			get { return x; }
			set 
			{ 
				x = value; 
				location.X = x;
			}
		}

		public int Y 
		{
			get { return y; }
			set 
			{ 
				y = value; 
				location.Y = y;
			}
		}

		public int Width 
		{
			get { return width; }
			set 
			{ 
				width = value; 
				size.Width = width;
			}
		}

		public int Height 
		{
			get { return height; }
			set 
			{ 
				height = value; 
				size.Height = height;
			}
		}

		public UILocation Location
		{
			get { return location; }
			set 
			{ 
				location = value; 
				x = location.X;
				y = location.Y;
			}
		}

		public UISize Size
		{
			get { return size; }
			set 
			{ 
				size = value; 
				width = size.Width;
				height = size.Height;
			}
		}

		public UIRect(int x, int y, int width, int height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.location = new UILocation(x, y);
			this.size = new UISize(width, height);
		}

		public override bool Equals(object obj)
		{
			var compareTo = (UIRect)obj;
			if (compareTo.X == X && compareTo.Y == Y && compareTo.Width == Width && compareTo.Height == Height)
				return true;

			return false;
		}

		public override int GetHashCode()
		{
			return X ^ Y ^ Width ^ Height;
		}

		public static bool operator==(UIRect a, UIRect b)
		{
			if(Object.ReferenceEquals(a, b))
				return true;

			if(((object)a == null) || ((object)b == null))
				return false;

			return a.Equals(b);
		}

		public static bool operator!=(UIRect a, UIRect b)
		{
			return !(a == b);
		}

		public static UIRect operator+(UIRect rect, UIPadding padding)
		{
			return new UIRect(
				rect.X, 
				rect.Y, 
				rect.Width + padding.Width, 
				rect.Height + padding.Height);
		}

		public static UIRect operator-(UIRect rect, UIPadding padding)
		{
			return new UIRect(
				rect.X + padding.Left, 
				rect.Y + padding.Top, 
				rect.Width - padding.Width, 
				rect.Height - padding.Height);
		}
	}
}