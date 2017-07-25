namespace WellFired.Guacamole.Image
{
    public interface IImageSource
    {
        string Filename { get; }
        ImageShapeData ImageShapeData { get; }
    }
}