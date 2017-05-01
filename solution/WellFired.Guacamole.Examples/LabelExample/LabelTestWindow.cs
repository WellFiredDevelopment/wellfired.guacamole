using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.LabelExample
{
	public class LabelTestWindow : Window
	{
		public LabelTestWindow()
		{
			Padding = UIPadding.Of(5);
		    Content = new Label { Text = "Sausages" };
		}
	}
}