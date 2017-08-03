using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Image;

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

#if DEBUG_TEXTURE_CREATION
		private static int _counter;
#endif

		[PublicAPI]
		public static Texture2D CreateRoundedTexture(
			int width,
			int height,
			UIColor backgroundColor,
			UIColor outlineColor,
			double radius,
			double thickness,
			CornerMask cornerMask,
			OutlineMask outlineMask)
		{
			var result = new Texture2D(width, height)
			{
				wrapMode = TextureWrapMode.Clamp
			};

			var pixelData = radius < 3 
				? ImageData.BuildSquare(width, height, backgroundColor, outlineColor, thickness) 
				: ImageData.BuildRounded(width, height, backgroundColor, outlineColor, radius, thickness, cornerMask, outlineMask);
			
			var unityPixelData = new Color[pixelData.Length];
			for (var index = 0; index < unityPixelData.Length; index++)
				unityPixelData[index] = pixelData[index].ToUnityColor();

			result.SetPixels(unityPixelData);
			result.Apply();
			result.hideFlags = HideFlags.HideAndDontSave;

#if DEBUG_TEXTURE_CREATION
			var bytes = result.EncodeToPNG();
			File.WriteAllBytes($"Assets/file{_counter}.png", bytes);
			_counter++;
#endif

			return result;
		}
	}
}