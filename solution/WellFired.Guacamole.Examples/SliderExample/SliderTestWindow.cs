using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.SliderExample
{
	public class SliderTestWindow : Window
	{
		public SliderTestWindow()
		{
			Padding = UIPadding.Of(5);

			Content = new Slider
			{
				MinValue = 0.0,
				MaxValue = 10.0,
				Value = 5.0
			};
		}
	}
}