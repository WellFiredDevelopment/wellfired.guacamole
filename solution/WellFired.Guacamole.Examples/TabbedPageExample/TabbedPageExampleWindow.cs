using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.TabbedPageExample
{
	public class TabbedPageExampleWindow : Window
	{
		public TabbedPageExampleWindow()
		{
			Padding = UIPadding.Of(5);

			var tabbedPage = new TabbedPage<string> {
				ItemSource = ItemSource<string>.From("Test1", "Test2", "Test3")
			};
			
			Content = tabbedPage;
		}
	}
}