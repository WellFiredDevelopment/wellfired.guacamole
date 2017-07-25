using UnityEngine;
using WellFired.Guacamole.Image;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
    public static class ImageSourceExtensions
    {   
        public static Texture2D ToUnityTexture(this IImageSource source)
        {
            var tex = new Texture2D(2, 2);
            tex.LoadImage(((ImageSource) source).Image.Data);
            return tex;
        }
    }
}