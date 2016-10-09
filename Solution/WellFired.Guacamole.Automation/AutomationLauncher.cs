using System;
using System.Linq;

namespace WellFired.Guacamole.Automation
{
	public class AutomationLauncher
	{
		public IAutomation Automation { get; }

		public AutomationLauncher()
		{
			var type = typeof(IAutomation);
			var automationPlatform = AppDomain.CurrentDomain
				.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.First(p => type.IsAssignableFrom(p));

			Automation = (IAutomation)Activator.CreateInstance(automationPlatform);
		}
	}
}
