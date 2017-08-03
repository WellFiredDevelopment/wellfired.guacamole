using System;
using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Drawing
{
	public class ImageFill
	{
		public enum FillStyle
		{
			Linear,
			Queue,
			Recursive
		}

		private bool[,] _pixelsChecked;

		public void Fill(UIImageRaw rawImage, Pixel sourcePoint, UIColor fillColor, FillStyle fillStyle)
		{
			var color = rawImage[sourcePoint.X, sourcePoint.Y];
			_pixelsChecked = new bool[rawImage.Width + 1, rawImage.Height + 1];

			switch (fillStyle)
			{
				case FillStyle.Linear:
					LinearFloodFill4(rawImage, sourcePoint.X, sourcePoint.Y, fillColor, color);
					break;
				case FillStyle.Queue:
					QueueFloodFill(rawImage, sourcePoint.X, sourcePoint.Y, fillColor, color);
					break;
				case FillStyle.Recursive:
					RecursiveFloodFill4(rawImage, sourcePoint.X, sourcePoint.Y, fillColor, color);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(fillStyle), fillStyle, null);
			}
		}

		private void LinearFloodFill4(UIImageRaw rawImage, int x, int y, UIColor fillColor, UIColor startingColor)
		{
			// Right Edge
			var localMinX = x;
			while (true)
			{
				rawImage[localMinX, y] = fillColor;
				_pixelsChecked[localMinX, y] = true;
				localMinX--;

				if ((localMinX < 0) ||
				    (rawImage[localMinX, y] != startingColor) ||
				    _pixelsChecked[localMinX, y])
					break;
			}
			localMinX++;

			// Left edge
			var localMaxX = x;
			while (true)
			{
				rawImage[localMaxX, y] = fillColor;
				_pixelsChecked[localMaxX, y] = true;
				localMaxX++;

				if ((localMaxX > rawImage.Width) ||
				    (rawImage[localMaxX, y] != startingColor) ||
				    _pixelsChecked[localMaxX, y])
					break;
			}
			localMaxX--;

			// Loop up and down           
			for (var currentX = localMinX; currentX <= localMaxX; currentX++)
			{
				// Loop up.
				if ((y > 0) && (rawImage[currentX, y - 1] == startingColor) && (_pixelsChecked[currentX, y - 1] == false))
					LinearFloodFill4(rawImage, currentX, y - 1, fillColor, startingColor);

				// Loop down.
				if ((y < rawImage.Height) && (rawImage[currentX, y + 1] == startingColor) &&
				    (_pixelsChecked[currentX, y + 1] == false))
					LinearFloodFill4(rawImage, currentX, y + 1, fillColor, startingColor);
			}
		}

		private static void QueueFloodFill(UIImageRaw rawImage, int i, int i1, UIColor fillColor, UIColor color)
		{
			throw new NotImplementedException();
		}

		private static void RecursiveFloodFill4(UIImageRaw rawImage, int i, int i1, UIColor fillColor, UIColor color)
		{
			throw new NotImplementedException();
		}
	}
}