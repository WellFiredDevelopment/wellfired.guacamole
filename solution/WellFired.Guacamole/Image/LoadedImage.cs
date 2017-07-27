using System.IO;

namespace WellFired.Guacamole.Image
{
    public class LoadedImage
    {
        public ImageType Type { get; set; }
        public byte[] Data { get; set; }
        
        private LoadedImage(ImageType imageType, byte[] data)
        {
            Type = imageType;
            Data = data;
        }

        public static LoadedImage From(IImageSourceWrapper imageSourceWrapper)
        {
            return new LoadedImage(imageSourceWrapper.ImageType, imageSourceWrapper.Data);
        }
    }
}