using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

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

			var enabledSlider = new Slider();

			var disabledSlider = new Slider
			{
				Enabled = false
			};

			var enabledLabel = new Label
			{
				Text = "Enabled"
			};

			var disabledLabel = new Label
			{
				Text = "Disabled",
				Enabled = false
			};

			Content = new LayoutView
			{
			    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
			    Spacing = 5,
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Expand,
				Children =
				{
					enabledButton,
					disabledButton,
					enabledSlider,
					disabledSlider,
					enabledLabel,
					disabledLabel
				}
			};
		}
	}
}