namespace WellFired.Guacamole.Image
{
    public class Image
    {
        public byte[] Data { get; set; }

        public void From(byte[] data)
        {
            Data = data;
        }
    }
}