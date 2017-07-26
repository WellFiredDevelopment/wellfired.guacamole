using JetBrains.Annotations;
using UnityEngine;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
	public static class Texture2DExtensions
	{
		[PublicAPI]
		public static Texture2D CreateTexture(int width, int height, Color colour)
		{
			var pixelColors = new Color[width*height];
			for (var i = 0; i < pixelColors.Length; i++)
				pixelColors[i] = colour;

			var result = new Texture2D(width, height)
			{
				wrapMode = TextureWrapMode.Repeat
			};

			result.SetPixels(pixelColors);
			result.Apply();
			result.hideFlags = HideFlags.HideAndDontSave;

			return result;
		}

		[PublicAPI]
		public static Texture2D CreateRoundedTexture(
			int width,
			int height,
			UIColor backgroundColor,
			UIColor outlineColor,
			double radius,
			CornerMask cornerMask)
		{
			var result = new Texture2D(width, height)
			{
				wrapMode = TextureWrapMode.Clamp
			};

			var pixelData = ImageData.BuildRounded(width, height, backgroundColor, outlineColor, radius, cornerMask);
			var unityPixelData = new Color[pixelData.Length];
			for (var index = 0; index < unityPixelData.Length; index++)
				unityPixelData[index] = pixelData[index].ToUnityColor();

			result.SetPixels(unityPixelData);
			result.Apply();
			result.hideFlags = HideFlags.HideAndDontSave;

			return result;
		}
	}
}