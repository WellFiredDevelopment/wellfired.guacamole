using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.UIBindingExample
{
	// ReSharper disable once InconsistentNaming
	public class UIBindingTestWindow : Window
	{
		public UIBindingTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			var destinationElement = new TextEntryView();

			var sourceElement = new SliderView
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