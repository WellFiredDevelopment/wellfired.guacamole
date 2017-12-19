using WellFired.Guacamole.Cells;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Examples.Intermediate.KeyValueListViewExample.View
{
	public class SettingCell : KeyValueCell
	{
		public SettingCell()
		{
			Bind(KeyTextProperty, "Setting", BindingMode.ReadOnly);
			Bind(ValueTextProperty, "Value", BindingMode.ReadOnly);
		}
	}
}