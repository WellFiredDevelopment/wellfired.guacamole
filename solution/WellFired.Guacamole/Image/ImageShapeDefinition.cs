using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Image
{
    public class ImageShapeDefinition
    {
        public ImageShape Shape { get; set; }
        public UIColor Color { get; set; }
        public int Size { get; set; }
        public UIColor OutlineColor { get; set; }

        public static ISourceHandler DefaultHandler => new ImageShapeDefinitionHandler(
            new ImageShapeDefinition
            {
                Shape = ImageShape.Circle,
                Size = 64,
                Color = UIColor.Burlywood,
                OutlineColor = UIColor.BlueViolet
            });
    }
}