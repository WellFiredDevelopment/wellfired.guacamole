using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.SliderExample
{
	public class SliderTestWindow : Window
	{
		public SliderTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			Content = new SliderView
			{
				MinValue = 0.0,
				MaxValue = 10.0,
				Value = 5.0
			};
		}
	}
}