namespace WellFired.Guacamole.Extensions
{
    public static class FloatExtensions
    {
        public static byte AsByte(this float value)
        {
            return (byte)(value * 255.0f);
        }
    }
}