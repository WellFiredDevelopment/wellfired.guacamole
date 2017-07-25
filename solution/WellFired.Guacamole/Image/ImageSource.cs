using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Image
{
    public class ImageSource : IImageSource
    {
        public string Filename { get; }
        public ImageShapeData ImageShapeData { get; }

        private ImageSource(string location)
        {
            Filename = location;
        }

        private ImageSource(ImageShapeData imageShapeData)
        {
            ImageShapeData = imageShapeData;
        }

        /// <summary>
        /// The image passed should be a per platform image location, see the documentation for your desired platform for more information.
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static IImageSource From(string location)
        {
            return new ImageSource(location);
        }

        public static IImageSource From(ImageShape shape, int size, UIColor color)
        {
            return new ImageSource(new ImageShapeData { Shape = shape, Size = size, Color = color });
        }
    }
}