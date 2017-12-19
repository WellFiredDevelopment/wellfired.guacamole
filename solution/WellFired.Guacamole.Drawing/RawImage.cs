namespace WellFired.Guacamole.Drawing
{
	public class RawImage
	{
		public int Stride => 4;

		public byte[] Data { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public int Length => Data.Length;

		public byte this[int i]
		{
			get => Data[i];
			set => Data[i] = value;
		}

		public ByteColor this[int x, int y]
		{
			get
			{
				var index = (Width * (Height - y - 1) + x) * Stride;
				return new ByteColor(Data[index + 0], Data[index + 1], Data[index + 2], Data[index + 3]);
			}
			set
			{
				var index = (Width * (Height - y - 1) + x) * Stride;
				Data[index + 0] = value.R;
				Data[index + 1] = value.G;
				Data[index + 2] = value.B;
				Data[index + 3] = value.A;
			}
		}
	}
}