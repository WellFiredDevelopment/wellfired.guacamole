using System;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor
{
	public class LaunchableApplication
	{
		protected static IApplication Launch<T>(UIRect uiRect, UISize minSize, string title = null, bool allowMultiple = true, string applicationName = "Guacamole", Type persistantType = null) where T : Window
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(T),
				UIRect = uiRect,
				MinSize = minSize,
				Title = title,
				AllowMultiple = allowMultiple,
				ApplicationName = applicationName
			};

			return persistantType != null ? application.Launch(context, persistantType) : application.Launch(context);
		}
	}
}