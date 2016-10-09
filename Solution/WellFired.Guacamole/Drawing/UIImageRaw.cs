using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Drawing
{
	public class UIImageRaw
	{
		public UIColor[] Data { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public int Length => Data.Length;

		public UIColor this[int i]
		{
			get { return Data[i]; }
			set { Data[i] = value; }
		}

		public UIColor this[int x, int y]
		{
			get { return Data[Width*(Height - y - 1) + x]; }
			set { Data[Width*(Height - y - 1) + x] = value; }
		}
	}
}