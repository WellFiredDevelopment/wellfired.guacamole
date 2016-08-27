using WellFired.Guacamole.Layout;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Examples.StylingExample
{
	public class StylingExampleWindow : Window
	{
		public StylingExampleWindow()
		{
			Padding = new UIPadding(5);

			var enabledButton = new Button
			{
				Text = "Enabled"
			};

			var disabledButton = new Button
			{
				Text = "Disabled",
				Enabled = false
			};

			var enabledSlider = new Slider
			{

			};

			var disabledSlider = new Slider
			{
				Enabled = false
			};

			Content = new AdjacentLayout
			{
				Spacing = 5,
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Expand,
				Children = {
					enabledButton,
					disabledButton,
					enabledSlider,
					disabledSlider
				}
			};
		}
	}
}