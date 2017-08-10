using WellFired.Guacamole.Drawing.Shapes;

namespace WellFired.Guacamole.Drawing.Layer
{
    public class Layer
    {
        public int Size { get; set; }
        public byte[] Data { get; set; }

        public Layer(byte [] data)
        {
            Size = data.Length;
            Data = data;
        }

        public Layer(int width, int height, IRasterizableShape shape)
        {
            Size = width * height * 4;
            Data = new byte[Size];
            shape.Rasterize(Data, width, height);
        }
    }
}