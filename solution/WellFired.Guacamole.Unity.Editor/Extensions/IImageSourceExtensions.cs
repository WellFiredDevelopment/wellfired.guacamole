using System;
using System.IO;
using UnityEngine;
using WellFired.Guacamole.Image;
using WellFired.Guacamole.Unity.Editor.Exception;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
    public static class ImageSourceExtensions
    {
        private const string ExtraPath = "/GuacamoleApplication/Editor/";

        public static Texture BuildTexture(this IImageSource source)
        {
            try
            {
                var textureBytes = File.ReadAllBytes(UnityEngine.Application.dataPath + ExtraPath + source.Filename);
                var tex = new Texture2D(2, 2);
                tex.LoadImage(textureBytes);
                return tex;
            }
            catch (FileNotFoundException)
            {
                throw new ImageSourceCouldntFindFileException(source.Filename);
            }
            catch (UnauthorizedAccessException)
            {
                throw new ImageSourceDoesntHaveAccessException(source.Filename);
            }
        }
    }
}