using System.IO;

namespace WellFired.Guacamole.Image
{
    public class ImageSourceWrapper : IImageSourceWrapper
    {
        public Stream Stream { get; }
        public ImageType ImageType { get; }

        public ImageSourceWrapper(Stream stream, ImageType imageType)
        {
            Stream = stream;
            ImageType = imageType;
        }
    }
}