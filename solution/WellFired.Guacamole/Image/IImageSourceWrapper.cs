namespace WellFired.Guacamole.Image
{
    public interface IImageSourceWrapper
    {
        byte[] Data { get; }
        ImageType ImageType { get; }
    }
}