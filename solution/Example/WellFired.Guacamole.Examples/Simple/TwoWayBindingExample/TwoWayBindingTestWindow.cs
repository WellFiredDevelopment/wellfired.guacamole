using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.TwoWayBindingExample
{
	public class TwoWayBindingTestWindow : Window
	{
		public TwoWayBindingTestWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
		{
			Padding = UIPadding.Of(5);

			var boundTextEntry = new TextEntry();

			Content = boundTextEntry;
			BindingContext = persistantData;

			boundTextEntry.Bind(TextEntry.TextProperty, "BoundText", BindingMode.TwoWay);
		}
	}
}