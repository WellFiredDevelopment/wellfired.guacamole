using System.IO;

namespace WellFired.Guacamole.Image
{
    public class ImageSourceWrapper : IImageSourceWrapper
    {
        public byte[] Data { get; }
        public ImageType ImageType { get; }

        public ImageSourceWrapper(Stream stream, ImageType imageType)
        {
            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                
                Data = ms.ToArray();
            }

            stream.Close();
            ImageType = imageType;
        }
    }
}