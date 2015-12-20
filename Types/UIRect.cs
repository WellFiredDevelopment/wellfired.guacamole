namespace WellFired.Guacamole
{
	public class UIRect
	{
		private int x;
		private int y;
		private int width;
		private int height;
		public UILocation location;
		public UISize size;

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

		public UIRect(UILocation location, UISize size)
		{
			Location = location;
			Size = size;
		}
	}
}