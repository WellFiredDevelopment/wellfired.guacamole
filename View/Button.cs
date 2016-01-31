namespace WellFired.Guacamole
{
	public class Button : ViewBase
	{
		public string Text
		{
			get;
			set;
		}

		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 40, 40);
		}
	}
}