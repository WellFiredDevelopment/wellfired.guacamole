using System;

namespace WellFired.Guacamole.Drawing
{
	public class ImageFill
	{
		private bool[,] _pixelsChecked;

		public void Fill(RawImage image, Pixel sourcePoint, ByteColor fillColor, FillStyle fillStyle)
		{
			var color = image[sourcePoint.X, sourcePoint.Y];
			_pixelsChecked = new bool[image.Width, image.Height];
			switch (fillStyle)
			{
				case FillStyle.Linear:
					LinearFloodFill4(image, sourcePoint.X, sourcePoint.Y, fillColor, color);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(fillStyle), fillStyle, null);
			}
		}

		private void LinearFloodFill4(RawImage image, int x, int y, ByteColor fillColor, ByteColor startingColor)
		{	
			// Right Edge
			var localMinX = x;
			while (localMinX >= 0)
			{
				image[localMinX, y] = fillColor;
				_pixelsChecked[localMinX, y] = true;
				localMinX--;
				
				if (localMinX < 0 || IsNotEqual(image, localMinX, y, startingColor) || _pixelsChecked[localMinX, y])
					break;
			}
			localMinX++;

			// Left edge
			var localMaxX = x;
			while (localMaxX < image.Width)
			{
				image[localMaxX, y] = fillColor;
				_pixelsChecked[localMaxX, y] = true;
				localMaxX++;

				if (localMaxX >= image.Width || IsNotEqual(image, localMaxX, y, startingColor) || _pixelsChecked[localMaxX, y])
					break;
			}
			localMaxX--;

			// Loop up and down           
			for (var currentX = localMinX; currentX <= localMaxX; currentX++)
			{
				// Loop up.
				if (y - 1 >= 0 && IsEqual(image, currentX, y - 1, startingColor) && !_pixelsChecked[currentX, y - 1])
					LinearFloodFill4(image, currentX, y - 1, fillColor, startingColor);

				// Loop down.
				if (y + 1 < image.Height && IsEqual(image, currentX, y + 1, startingColor) && !_pixelsChecked[currentX, y + 1])
					LinearFloodFill4(image, currentX, y + 1, fillColor, startingColor);
			}
		}

		private static bool IsEqual(RawImage image, int x, int y, ByteColor testColor)
		{	
			var index = (image.Width * (image.Height - y - 1) + x) * image.Stride;
			return
				image.Data[index + 0] == testColor.R
				&& image.Data[index + 1] == testColor.G
				&& image.Data[index + 2] == testColor.B
				&& image.Data[index + 3] == testColor.A;
		}

		private static bool IsNotEqual(RawImage image, int x, int y, ByteColor testColor)
		{
			return !IsEqual(image, x, y, testColor);
		}
	}
}