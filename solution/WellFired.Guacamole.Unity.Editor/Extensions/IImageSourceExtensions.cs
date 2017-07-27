using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
    public static class ImageSourceExtensions
    {   
        public static async Task<Texture2D> ToUnityTexture(this IImageSource source, LoadedImage loadedImage)
        {
            Texture2D image = null;
            Device.ExecuteOnMainThread(() =>
            {
                switch (loadedImage.Type)
                {
                    case ImageType.Image:
                        image = new Texture2D(2, 2);
                        image.LoadImage(loadedImage.Data);
                        break;
                    case ImageType.Raw:
                        // [TODO] Don't assume square images for Raw.
                        var size = (int) Math.Sqrt(loadedImage.Data.Length / 4.0f);
                        // [TODO] Don't assume texture format for Raw.
                        const TextureFormat format = TextureFormat.RGBA32;

                        image = new Texture2D(size, size, format, false);
                        image.LoadRawTextureData(loadedImage.Data);
                        image.Apply();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });

            const int msTimeout = 5000;
            var msTickCount = 0;
            while (!image)
            {
                await TaskEx.Delay(1);
                msTickCount++;

                // We don't want our thread to loop forever, lets have a timeout
                if (msTickCount > msTimeout)
                    throw new TimeoutException();
            }

            return image;
        }
    }
}