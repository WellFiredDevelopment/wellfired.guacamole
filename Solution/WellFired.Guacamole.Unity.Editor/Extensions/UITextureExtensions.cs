using JetBrains.Annotations;
using UnityEngine;
using WellFired.Guacamole.Drawing;
using WellFired.Guacamole.Types;
using Rect = WellFired.Guacamole.Drawing.Rect;

namespace WellFired.Guacamole.Unity.Editor.Extensions
{
    public static class Texture2DExtensions
    {
        [PublicAPI]
        public static Texture2D CreateTexture(int width, int height, Color colour)
        {
            var pixelColors = new Color[width * height];
            for(var i = 0; i < pixelColors.Length; i++)
                pixelColors[i] = colour;

            var result = new Texture2D(width, height) {
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
            var result = new Texture2D(width, height) {
                wrapMode = TextureWrapMode.Clamp
            };

            var path = new GraphicsPath();

            var diameter = radius * 2;

            var topLeft = (cornerMask & CornerMask.TopLeft) != 0;
            var topRight = (cornerMask & CornerMask.TopRight) != 0;
            var bottomRight = (cornerMask & CornerMask.BottomRight) != 0;
            var bottomLeft = (cornerMask & CornerMask.BottomLeft) != 0;

            if(topLeft)
            {
                var topLeftRect = new Rect(0, 0, diameter, diameter);
                path.AddArc(topLeftRect, 180, 90);

                path.AddLine(new Vector(radius, 0), topRight ? new Vector(width - radius, 0) : new Vector(width, 0));
            }
            else
            {
                path.AddLine(new Vector(0, 0), topRight ? new Vector(width - radius, 0) : new Vector(width, 0));
            }

            if(topRight)
            {
                var topRightRect = new Rect(width - diameter, 0, diameter, diameter);
                path.AddArc(topRightRect, 90, 90);

                path.AddLine(
                    new Vector(width, radius),
                    bottomRight ? new Vector(width, height - radius) : new Vector(width, height));
            }
            else
            {
                path.AddLine(
                    new Vector(width, 0),
                    bottomRight ? new Vector(width, height - radius) : new Vector(width, height));
            }

            if(bottomRight)
            {
                var bottomRightRect = new Rect(width - diameter, height - diameter, diameter, diameter);
                path.AddArc(bottomRightRect, 0, 90);

                path.AddLine(
                    new Vector(width - radius, height),
                    bottomLeft ? new Vector(radius, height) : new Vector(0, height));
            }
            else
            {
                path.AddLine(new Vector(width, height), bottomLeft ? new Vector(radius, height) : new Vector(0, height));
            }

            if(bottomLeft)
            {
                var bottomLeftRect = new Rect(0, height - diameter, diameter, diameter);
                path.AddArc(bottomLeftRect, 270, 90);

                path.AddLine(new Vector(0, height - radius), topLeft ? new Vector(0, radius) : new Vector(0, 0));
            }
            else
            {
                path.AddLine(new Vector(0, height), topLeft ? new Vector(0, radius) : new Vector(0, 0));
            }

            var pixelData = path.Draw(width, height, backgroundColor, outlineColor);

            var unityPixelData = new Color[pixelData.Length];
            for(var index = 0; index < unityPixelData.Length; index++)
                unityPixelData[index] = pixelData[index].ToUnityColor();

            result.SetPixels(unityPixelData);
            result.Apply();
            result.hideFlags = HideFlags.HideAndDontSave;

            return result;
        }
    }
}