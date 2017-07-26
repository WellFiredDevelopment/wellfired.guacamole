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
            byte[] data;
            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = loadedImage.Stream.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                
                data = ms.ToArray();
            }
            loadedImage.Stream.Close();

            Texture2D image = null;
            switch (loadedImage.Type)
            {
                case ImageType.Image:
                    Device.ExecuteOnMainThread(() =>
                    {
                        image = new Texture2D(2, 2);
                        image.LoadImage(data);
                    });
                    break;
                case ImageType.Raw:
                    Device.ExecuteOnMainThread(() =>
                    {
                        // [TODO] Don't assume square images for Raw.
                        var size = (int)Math.Sqrt(data.Length / 4.0f);
                        // [TODO] Don't assume texture format for Raw.
                        const TextureFormat format = TextureFormat.RGBA32;
                    
                        image = new Texture2D(size, size, format, false);
                        image.LoadRawTextureData(data);
                        image.Apply();
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

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