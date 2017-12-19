using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Intermediate.KeyValueListViewExample.View
{
	public class SettingsListView : ListView
	{
		public SettingsListView()
		{
			HorizontalLayout = LayoutOptions.Fill;
			VerticalLayout = LayoutOptions.Expand;
			Orientation = OrientationOptions.Vertical;
			EntrySize = 20;
			Spacing = 5;
			ItemTemplate = DataTemplate.Of(typeof(SettingCell));
			Bind(ItemSourceProperty, "Settings", BindingMode.ReadOnly);
		}
	}
}