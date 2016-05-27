using System;

namespace WellFired.Guacamole.Drawing
{
	public class GraphicsPath
	{
		private Rect _rect;
		private readonly float _cornerRounding;
		private UIColor _backgroundColor;

		public GraphicsPath(Rect rect)
		{
			_rect = rect;
		}

		public GraphicsPath(Rect rect, float cornerRounding)
		{
			_rect = rect;
			_cornerRounding = cornerRounding;
		}

		public void FillWith(UIColor backgroundColor)
		{
			_backgroundColor = backgroundColor;
		}

		public void Fill(ref UIColor[] pixelData, int width, int height)
		{
			var size = width*height;
			if(pixelData.Length != size)
				throw new Exception("Width and Height doesn't match the lengh of the passed data.");


		}
	}
}