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
	    public static byte[] BuildEllipse(int width, int height, UIColor backgroundColor, UIColor outlineColor, double thickness)
	    {
		    var path = new GraphicsPath();
		    
		    path.FromRectDefinedEllipse(new Rect(1, 1, width-3, height-3));

		    return path.Draw(width, height);
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
	    public static byte[] BuildCircle(int width, int height, UIColor backgroundColor, UIColor outlineColor, double thickness)
	    {
		    var path = new GraphicsPath();

		    var visibleWidth = width - 2;
		    var visibleHeight = height - 2;
		    var radius = (Math.Min(visibleHeight, visibleWidth) - 1) / 2.0;
		    const int xOffset = 1;
		    const int yOffset = 1;
		    
		    path.FromCircle(new Vector(
				    (visibleWidth - xOffset) / 2 + xOffset,
				    (visibleHeight - yOffset) / 2 + yOffset),
			    radius, 
			    thickness,
			    backgroundColor.ToByteColor(), 
			    outlineColor.ToByteColor());
		    
		    return path.Draw(width, height);
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
	    public static byte[] BuildCircleQuarter(QuarterCircle.Quarter quarter, int width, int height, UIColor backgroundColor, UIColor outlineColor, double thickness)
	    {
		    var path = new GraphicsPath();

		    var visibleWidth = width - 2;
		    var visibleHeight = height - 2;
		    var radius = (Math.Min(visibleHeight, visibleWidth) - 1) / 2.0;
		    const int xOffset = 1;
		    const int yOffset = 1;
		    path.FromCircleQuarter(
			    quarter,
			    new Vector(
				    (visibleWidth - xOffset) / 2 + xOffset,
				    (visibleHeight - yOffset) / 2 + yOffset),
			    radius,
			    thickness,
			    backgroundColor.ToByteColor(),
			    outlineColor.ToByteColor());

		    return path.Draw(width, height);
	    }

	    /// <summary>
	    /// A helpful utility method that allows us to quickly create a square texture
	    /// </summary>
	    /// <param name="width"></param>
	    /// <param name="height"></param>
	    /// <param name="backgroundColor"></param>
	    /// <param name="outlineColor"></param>
	    /// <param name="thickness"></param>
	    /// <param name="outlineMask"></param>
	    /// <returns></returns>
	    public static byte[] BuildRect(int width, int height, UIColor backgroundColor, UIColor outlineColor, double thickness, OutlineMask outlineMask)
	    {
		    var path = new GraphicsPath();
		    
		    path.FromRect(new Rect(0, 0, width - 1, height - 1), thickness, backgroundColor.ToByteColor(), outlineColor.ToByteColor(), outlineMask);
		    
		    return path.Draw(width, height);
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
	    public static byte[] BuildRounded(int width, int height, UIColor backgroundColor, UIColor outlineColor, double radius, double thickness, CornerMask cornerMask, OutlineMask outlineMask)
        {
			var path = new GraphicsPath();		        
	        
	        path.FromRoundedCornerRect(new Rect(0, 0, width - 1, height - 1), radius, thickness, backgroundColor.ToByteColor(), outlineColor.ToByteColor(), cornerMask, outlineMask);

	        return path.Draw(width, height);
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

	    public static UIColor[] FromRgbaByteData(byte[] colors)
	    {
		    var colorArray = new UIColor[colors.Length / 4];

		    var counter = 0;
		    for (var i = 0; i < colorArray.Length; i++)
		    {
			    colorArray[i] = UIColor.FromRGBA(colors[counter + 0], colors[counter + 1], colors[counter + 2], colors[counter + 3]);
			    counter += 4;
		    }

		    return colorArray;
	    }
    }
}