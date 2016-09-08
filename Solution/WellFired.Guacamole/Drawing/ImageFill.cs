using System;
using WellFired.Guacamole.Types;

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

				if (localMinX < 0 || 
					rawImage[localMinX, y] != startingColor ||
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

				if (localMaxX > rawImage.Width || 
					rawImage[localMaxX, y] != startingColor ||
					_pixelsChecked[localMaxX, y])
					break;
			}
			localMaxX--;

			// Loop up and down           
			for(var currentX = localMinX; currentX <= localMaxX; currentX++)
			{
				// Loop up.
				if(y > 0 && rawImage[currentX, y - 1] == startingColor && _pixelsChecked[currentX, y - 1] == false)
					LinearFloodFill4(rawImage, currentX, y - 1, fillColor, startingColor);

				// Loop down.
				if (y < rawImage.Height && rawImage[currentX, y + 1] == startingColor && _pixelsChecked[currentX, y + 1] == false)
					LinearFloodFill4(rawImage, currentX, y + 1, fillColor, startingColor);
			}
		}

		private static void QueueFloodFill(UIImageRaw rawImage, int i, int i1, UIColor fillColor, UIColor color)
		{

		}

		private static void RecursiveFloodFill4(UIImageRaw rawImage, int i, int i1, UIColor fillColor, UIColor color)
		{

		}

		/*

		unsafe void LinearFloodFill4(byte* scan0, int x, int y, Size bmpsize,
										int stride, byte* startcolor)
		{

			//offset the pointer to the point passed in
			int* p = (int*)(scan0 + (CoordsToIndex(x, y, stride)));


			//FIND LEFT EDGE OF COLOR AREA
			int LFillLoc = x; //the location to check/fill on the left
			int* ptr = p; //the pointer to the current location
			while (true)
			{
				ptr[0] = m_fillcolor;      //fill with the color
				PixelsChecked[LFillLoc, y] = true;
				LFillLoc--;               //de-increment counter
				ptr -= 1;                      //de-increment pointer
				if (LFillLoc <= 0 ||
					!CheckPixel((byte*)ptr, startcolor) ||
					(PixelsChecked[LFillLoc, y]))
					//exit loop if we're at edge of bitmap or color area
					break;

			}
			LFillLoc++;

			//FIND RIGHT EDGE OF COLOR AREA
			int RFillLoc = x; //the location to check/fill on the left
			ptr = p;
			while (true)
			{
				ptr[0] = m_fillcolor; //fill with the color
				PixelsChecked[RFillLoc, y] = true;
				RFillLoc++;          //increment counter
				ptr += 1;                 //increment pointer
				if (RFillLoc >= bmpsize.Width ||
					!CheckPixel((byte*)ptr, startcolor) ||
					(PixelsChecked[RFillLoc, y]))
					//exit loop if we're at edge of bitmap or color area
					break;

			}
			RFillLoc--;


			//START THE LOOP UPWARDS AND DOWNWARDS            
			ptr = (int*)(scan0 + CoordsToIndex(LFillLoc, y, stride));
			for (int i = LFillLoc; i <= RFillLoc; i++)
			{
				//START LOOP UPWARDS
				//if we're not above the top of the bitmap 
				//and the pixel above this one is within the color tolerance
				if (y > 0 &&
				 CheckPixel((byte*)(scan0 + CoordsToIndex(i, y - 1, stride)), startcolor) &&
				 (!(PixelsChecked[i, y - 1])))
					LinearFloodFill4(scan0, i, y - 1, bmpsize, stride, startcolor);

				//START LOOP DOWNWARDS
				if (y < (bmpsize.Height - 1) &&
				CheckPixel((byte*)(scan0 + CoordsToIndex(i, y + 1, stride)), startcolor) &&
				(!(PixelsChecked[i, y + 1])))
					LinearFloodFill4(scan0, i, y + 1, bmpsize, stride, startcolor);
				ptr += 1;
			}

		}

		///<SUMMARY>Sees if a pixel is within the color tolerance range.</SUMMARY>
		//px - a pointer to the pixel to check
		//startcolor - a pointer to the color of the pixel we started at
		unsafe bool CheckPixel(byte* px, byte* startcolor)
		{
			bool ret = true;
			for (byte i = 0; i < 3; i++)
				ret &= (px[i] >= (startcolor[i] - m_Tolerance[i])) &&
						px[i] <= (startcolor[i] + m_Tolerance[i]);
			return ret;
		}
	*/
	}
}