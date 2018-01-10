using System.Collections.Generic;
using System.ComponentModel;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Platform;

namespace WellFired.Guacamole.Examples.Intermediate.KeyValueListViewExample.ViewModel
{
	public class SettingsViewModel : ObservableBase, IBasicViewModel
	{
		private List<SettingBindingContext> _settings;
		
		[PublicAPI]
		public List<SettingBindingContext> Settings
		{
			get => _settings;
			set => SetProperty(ref _settings, value);
		}

		public void Inject(ILogger logger, INotifyPropertyChanged persistentData, IPlatformProvider platformProvider)
		{
			_settings = new List<SettingBindingContext>(new [] {
				new SettingBindingContext("API", "OpenGL"),
				new SettingBindingContext("Static Batching", "True"),
				new SettingBindingContext("Dynamic Batching", "False"),
				new SettingBindingContext("Backend", "IL2CPP"),
				new SettingBindingContext("Device", "IPad 2"),
				new SettingBindingContext("A very long setting to see how the key behave when it's longer than the space available", "IPad 2")
			});
		}
	}
}