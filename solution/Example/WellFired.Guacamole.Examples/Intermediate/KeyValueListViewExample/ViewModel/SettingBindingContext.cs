using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding.Cells;

namespace WellFired.Guacamole.Examples.Intermediate.KeyValueListViewExample.ViewModel
{
	public class SettingBindingContext : CellBindingContextBase
	{
		private string _setting;
		private string _value;
		
		[PublicAPI]
		public string Setting
		{
			get => _setting;
			set => SetProperty(ref _setting, value);
		}
		
		[PublicAPI]
		public string Value
		{
			get => _value;
			set => SetProperty(ref _value, value);
		}

		public SettingBindingContext(string setting, string value)
		{
			_setting = setting;
			_value = value;
		}
	}
}