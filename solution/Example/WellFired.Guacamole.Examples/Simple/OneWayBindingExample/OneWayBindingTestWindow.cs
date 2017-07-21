using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.OneWayBindingExample
{
	public class OneWayBindingTestWindow : Window
	{
		public OneWayBindingTestWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
		{
			Padding = UIPadding.Of(5);

			var boundTextEntry = new TextEntry();

			Content = boundTextEntry;

			BindingContext = persistantData;
			boundTextEntry.Bind(TextEntry.TextProperty, "BoundText");
		}
	}
}