namespace WellFired.Guacamole
{
	public class TextEntry : ViewBase
	{
		public string Label
		{
			get;
			set;
		}

		public string Text
		{
			get;
			set;
		}

		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 250, 20);
		}
	}
}