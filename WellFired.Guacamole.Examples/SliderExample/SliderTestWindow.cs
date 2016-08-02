namespace WellFired.Guacamole.Examples.TextField
{
	public class SliderTestWindow : Window
	{
		public SliderTestWindow()
		{
			Padding = new UIPadding(5);

			Content = new Slider
			{
				BackgroundColor = UIColor.White,
				MinValue = 0.0,
				MaxValue = 10.0,
				Value = 5.0
			};
		}
	}
}