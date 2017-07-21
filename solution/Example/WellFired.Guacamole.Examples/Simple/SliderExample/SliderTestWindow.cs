using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.SliderExample
{
	public class SliderTestWindow : Window
	{
		public SliderTestWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
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