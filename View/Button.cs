namespace WellFired.Guacamole
{
	public class Button : ViewBase
	{
		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 40, 40);
		}
	}
}