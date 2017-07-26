using System.IO;

namespace WellFired.Guacamole.Image
{
    public interface IImageSourceWrapper
    {
        Stream Stream { get; }
        ImageType ImageType { get; }
    }
}