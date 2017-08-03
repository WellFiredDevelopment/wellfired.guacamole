using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.TextFieldExample
{
	public class TextFieldTestWindow : Window
	{
		public TextFieldTestWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
		{
			Padding = UIPadding.Of(5);

			Content = new TextEntry
			{
				Text = "Test"
			};
		}
	}
}