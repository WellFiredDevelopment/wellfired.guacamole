using System;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Drawing;
using WellFired.Guacamole.Drawing.Shapes;
using WellFired.Guacamole.Extensions;

namespace WellFired.Guacamole.Image
{
    public static class ImageData
    {
	    /// <summary>
	    /// A helpful utility method that allows us to quickly create a elipse texture inside a rect.
	    /// </summary>
	    /// <param name="width"></param>
	    /// <param name="height"></param>
	    /// <param name="backgroundColor"></param>
	    /// <param name="outlineColor"></param>
	    /// <param name="thickness"></param>
	    /// <returns></returns>
	    public static UIColor[] BuildEllipse(int width, int height, UIColor backgroundColor, UIColor outlineColor, double thickness)
	    {
		    var path = new GraphicsPath(thickness);
		    
		    path.AddRectDefinedEllipse(new Rect(1, 1, width-3, height-3));

		    return path.Draw(width, height, backgroundColor, outlineColor);
	    }

	    /// <summary>
	    /// A helpful utility method that allows us to quickly create a circle texture
	    /// </summary>
	    /// <param name="width"></param>
	    /// <param name="height"></param>
	    /// <param name="backgroundColor"></param>
	    /// <param name="outlineColor"></param>
	    /// <param name="thickness"></param>
	    /// <returns></returns>
	    public static UIColor[] BuildCircle(int width, int height, UIColor backgroundColor, UIColor outlineColor, double thickness)
	    {
		    var path = new GraphicsPath(thickness);

		    var visibleWidth = width - 2;
		    var visibleHeight = height - 2;
		    var radius = (Math.Min(visibleHeight, visibleWidth) - 1) / 2.0;
		    const int xOffset = 1;
		    const int yOffset = 1;
		    path.AddCircle(
			    new Vector(
				    (visibleWidth - xOffset) / 2 + xOffset,
				    (visibleHeight - yOffset) / 2 + yOffset),
			    radius);

		    return path.Draw(width, height, backgroundColor, outlineColor);
	    }

	    /// <summary>
	    /// A helpful utility method that allows us to quickly create a quarter circle
	    /// </summary>
	    /// <param name="quarter"></param>
	    /// <param name="width"></param>
	    /// <param name="height"></param>
	    /// <param name="backgroundColor"></param>
	    /// <param name="outlineColor"></param>
	    /// <param name="thickness"></param>
	    /// <returns></returns>
	    public static UIColor[] BuildCircleQuarter(QuarterCircle.Quarter quarter, int width, int height, UIColor backgroundColor, UIColor outlineColor, double thickness)
	    {
		    var path = new GraphicsPath(thickness);

		    var visibleWidth = width - 2;
		    var visibleHeight = height - 2;
		    var radius = (Math.Min(visibleHeight, visibleWidth) - 1) / 2.0;
		    const int xOffset = 1;
		    const int yOffset = 1;
		    path.AddCircleQuarter(
			    quarter,
			    new Vector(
				    (visibleWidth - xOffset) / 2 + xOffset,
				    (visibleHeight - yOffset) / 2 + yOffset),
			    radius);

		    return path.Draw(width, height, backgroundColor, outlineColor);
	    }

	    /// <summary>
	    /// A helpful utility method that allows us to quickly create a square texture
	    /// </summary>
	    /// <param name="width"></param>
	    /// <param name="height"></param>
	    /// <param name="backgroundColor"></param>
	    /// <param name="outlineColor"></param>
	    /// <param name="thickness"></param>
	    /// <returns></returns>
	    public static UIColor[] BuildSquare(int width, int height, UIColor backgroundColor, UIColor outlineColor, double thickness)
	    {
		    var path = new GraphicsPath(thickness);
		    
		    var farLeft = thickness;
		    var farTop = thickness;
		    var farBottom = height - thickness;
		    var farRight = width - thickness;

		    if (Math.Abs(farBottom - height) < 0.001f)
			    farBottom--;
		    if (Math.Abs(farRight - width) < 0.001f)
			    farRight--;

		    path.AddLine(new Vector(farLeft, farTop), new Vector(farRight, farTop));
		    path.AddLine(new Vector(farRight, farTop), new Vector(farRight, farBottom));
		    path.AddLine(new Vector(farRight, farBottom), new Vector(farLeft, farBottom));
		    path.AddLine(new Vector(farLeft, farBottom), new Vector(farLeft, farTop));
		    
		    return path.Draw(width, height, backgroundColor, outlineColor);
	    }
	    
	    /// <summary>
	    /// This is a helpful Utility method that allows you to create a texture with rounded corners.
	    /// </summary>
	    /// <param name="width"></param>
	    /// <param name="height"></param>
	    /// <param name="backgroundColor"></param>
	    /// <param name="outlineColor"></param>
	    /// <param name="radius"></param>
	    /// <param name="thickness"></param>
	    /// <param name="cornerMask"></param>
	    /// <param name="outlineMask"></param>
	    /// <returns></returns>
	    public static UIColor[] BuildRounded(int width, int height, UIColor backgroundColor, UIColor outlineColor, double radius, double thickness, CornerMask cornerMask, OutlineMask outlineMask)
        {
			var path = new GraphicsPath(thickness);		        

			//var diameter = radius * 2;

			var topLeft = (cornerMask & CornerMask.TopLeft) != 0;
			var topRight = (cornerMask & CornerMask.TopRight) != 0;
			var bottomRight = (cornerMask & CornerMask.BottomRight) != 0;
			var bottomLeft = (cornerMask & CornerMask.BottomLeft) != 0;
	        
	        //var top = (outlineMask & OutlineMask.Top) != 0;
	        //var right = (outlineMask & OutlineMask.Right) != 0;
	        //var bottom = (outlineMask & OutlineMask.Bottom) != 0;
	        //var left = (outlineMask & OutlineMask.Left) != 0;

	        var farLeft = thickness - thickness / 2.0;
	        var farTop = thickness - thickness / 2.0;
	        var farRight = width - thickness + thickness / 2.0;

			if (topLeft)
			{
				path.AddCircleQuarter(QuarterCircle.Quarter.TopLeft, new Vector(radius, radius), radius);
				path.AddLine(new Vector(radius, farTop), topRight ? new Vector(width - 1 - radius, farTop) : new Vector(width - 1, farTop));
			}
			else
			{
				path.AddLine(new Vector(0, farTop), topRight ? new Vector(width - 1 - radius, farTop) : new Vector(width - 1, farTop));
			}
	        
			if (topRight)
			{
				path.AddCircleQuarter(QuarterCircle.Quarter.TopRight, new Vector(width - 1 - radius, radius), radius);
				path.AddLine(
					new Vector(width - 1, radius),
					bottomRight ? new Vector(width - 1, height - 1 - radius) : new Vector(width - 1, height - 1));
			}
			else
			{
				path.AddLine(
					new Vector(farRight, 0),
					bottomRight ? new Vector(farRight, height - 1 - radius) : new Vector(farRight, height - 1));
			}

			if (bottomRight)
			{
				path.AddCircleQuarter(QuarterCircle.Quarter.BottomRight, new Vector(width - 1 - radius, height - 1 - radius), radius);
				path.AddLine(
					new Vector(width - 1 - radius, height - 1),
					bottomLeft ? new Vector(radius, height - 1) : new Vector(0, height - 1));
			}
			else
			{
				path.AddLine(new Vector(width - 1, height - 1), bottomLeft ? new Vector(radius, height - 1) : new Vector(0, height - 1));
			}

			if (bottomLeft)
			{
				path.AddCircleQuarter(QuarterCircle.Quarter.BottomLeft, new Vector(radius, height - 1 - radius), radius);
				path.AddLine(new Vector(farLeft, height - 1 - radius), topLeft ? new Vector(farLeft, radius) : new Vector(farLeft, 0));
			}
			else
			{
				path.AddLine(new Vector(farLeft, height - 1), topLeft ? new Vector(farLeft, radius) : new Vector(farLeft, 0));
			}
	        
			return path.Draw(width, height, backgroundColor, outlineColor);
        }

	    public static byte[] ToRgbByteData(UIColor[] colors)
	    {
		    var byteArray = new byte[colors.Length * 3];
		    var runningCount = 0;
		    foreach (var color in colors)
		    {
			    byteArray[runningCount + 0] = color.R.AsByte();
			    byteArray[runningCount + 1] = color.G.AsByte();
			    byteArray[runningCount + 2] = color.B.AsByte();
			    runningCount += 3;
		    }

		    return byteArray;
	    }

	    public static byte[] ToRgbaByteData(UIColor[] colors)
	    {
		    var byteArray = new byte[colors.Length * 4];
		    var runningCount = 0;
		    foreach (var color in colors)
		    {
			    byteArray[runningCount + 0] = color.R.AsByte();
			    byteArray[runningCount + 1] = color.G.AsByte();
			    byteArray[runningCount + 2] = color.B.AsByte();
			    byteArray[runningCount + 3] = color.A.AsByte();
			    runningCount += 4;
		    }

		    return byteArray;
	    }

	    public static byte[] ToArgbByteData(UIColor[] colors)
	    {
		    var byteArray = new byte[colors.Length * 4];
		    var runningCount = 0;
		    foreach (var color in colors)
		    {
			    byteArray[runningCount + 0] = color.A.AsByte();
			    byteArray[runningCount + 1] = color.R.AsByte();
			    byteArray[runningCount + 2] = color.G.AsByte();
			    byteArray[runningCount + 3] = color.B.AsByte();
			    runningCount += 4;
		    }

		    return byteArray;
	    }
    }
}