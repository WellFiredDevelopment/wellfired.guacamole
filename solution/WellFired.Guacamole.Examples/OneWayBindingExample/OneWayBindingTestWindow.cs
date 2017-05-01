using System.ComponentModel;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.OneWayBindingExample
{
	public class OneWayBindingTestWindow : Window
	{
		public OneWayBindingTestWindow(INotifyPropertyChanged persistantData) : base(persistantData)
		{
			Padding = UIPadding.Of(5);

			var boundTextEntry = new TextEntry();

			Content = boundTextEntry;

			BindingContext = persistantData;
			boundTextEntry.Bind(TextEntry.TextProperty, "BoundText");
		}
	}
}