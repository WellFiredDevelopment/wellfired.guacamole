using System.ComponentModel;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Examples.Intermediate.KeyValueListViewExample.View;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Intermediate.KeyValueListViewExample
{
	public class KeyValueListViewTestWindow : Window
	{
		public KeyValueListViewTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider)
			: base(logger, persistantData, platformProvider)
		{
			Content = new SettingsListView();
		}
	}
}