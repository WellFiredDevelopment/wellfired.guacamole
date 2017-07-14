using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.UIBindingExample
{
	// ReSharper disable once InconsistentNaming
	public class UIBindingTestWindow : Window
	{
		public UIBindingTestWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
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
				HorizontalLayout = LayoutOptions.Fill,
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