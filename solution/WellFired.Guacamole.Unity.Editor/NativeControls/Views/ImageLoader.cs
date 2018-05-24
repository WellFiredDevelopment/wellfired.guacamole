using System;
using System.Threading.Tasks;
using UnityEngine;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Unity.Editor.NativeControls.Views
{
	public class ImageLoader
	{
		private readonly ImageCreatorHandler _handler = new ImageCreatorHandler();

		/// <summary>
		/// Load an image from an image source. Since loading is asynchronous, it's very possible that once the image is loaded
		/// the calling entity does not require it anymore. In this case a null value is returned.
		/// </summary>
		/// <param name="imageSource">The source from where the image is loaded</param>
		/// <param name="isImageStillAwaited">A delegate informing if the loaded image is still required or not by the calling entity</param>
		/// <returns></returns>
		public async Task<Texture2D> LoadImage(IImageSource imageSource, Func<bool> isImageStillAwaited)
		{
			var texture = await _handler.UpdatedImageSource(imageSource);
			
			return isImageStillAwaited() ? 
					texture :
					default(Texture2D);
		}
	}
}