using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.TabbedPageExample
{
	public class TabbedPageExampleWindow : Window
	{
		public TabbedPageExampleWindow(ILogger logger, INotifyPropertyChanged persistantData) 
			: base(logger, persistantData)
		{
			Padding = UIPadding.Of(5);

			var tabbedPage = new TabbedPage<string> {
				ItemSource = ItemSource<string>.From("Test1", "Test2", "Test3")
			};
			
			Content = tabbedPage;
		}
	}
}