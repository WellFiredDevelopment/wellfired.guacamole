using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor.Diagnostics;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor
{
	public class LaunchableApplication
	{
		protected static Application Launch<T>(UIRect uiRect, UISize minSize, string title = null, bool allowMultiple = true) where T : Window
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(T),
				UIRect = uiRect,
				MinSize = minSize,
				Title = title,
				AllowMultiple = allowMultiple
			};

			application.Launch(context);

			return application;
		}
	}
}