using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.OneWayBindingExample
{
	public class OneWayBindingTestWindow : Window
	{
		public OneWayBindingTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			Padding = UIPadding.Of(5);

			var boundTextEntry = new TextEntry();

			Content = boundTextEntry;

			BindingContext = persistantData;
			boundTextEntry.Bind(TextEntry.TextProperty, "BoundText");
		}
	}
}