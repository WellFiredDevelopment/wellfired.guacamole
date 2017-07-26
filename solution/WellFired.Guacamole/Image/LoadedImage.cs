using System.IO;

namespace WellFired.Guacamole.Image
{
    public class LoadedImage
    {
        public ImageType Type { get; set; }
        public Stream Stream { get; set; }
        
        private LoadedImage(ImageType imageType, Stream stream)
        {
            Type = imageType;
            Stream = stream;
        }

        public static LoadedImage From(IImageSourceWrapper imageSourceWrapper)
        {
            return new LoadedImage(imageSourceWrapper.ImageType, imageSourceWrapper.Stream);
        }
    }
}