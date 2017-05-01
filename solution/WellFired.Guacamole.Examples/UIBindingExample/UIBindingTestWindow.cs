using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.UIBindingExample
{
	// ReSharper disable once InconsistentNaming
	public class UIBindingTestWindow : Window
	{
		public UIBindingTestWindow()
		{
			Padding = UIPadding.Of(5);

			var destinationElement = new TextEntry();

			var sourceElement = new Slider
			{
				MinValue = 0,
				MaxValue = 32
			};

			Content = new LayoutView
			{
			    Layout = new AdjacentLayout { Orientation = OrientationOptions.Horizontal },
			    Children =
				{
					destinationElement,
					sourceElement
				}
			};

			destinationElement.BindingContext = sourceElement;

			destinationElement.Bind(CornerRadiusProperty, "Value");
		}
	}
}