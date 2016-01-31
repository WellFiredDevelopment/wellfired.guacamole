namespace WellFired.Guacamole
{
	public class NumberEntry : ViewBase
	{
		public string Label
		{
			get;
			set;
		}

		public float Number
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