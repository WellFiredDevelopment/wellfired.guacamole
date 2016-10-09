using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Unity.Editor
{
	public class LaunchableApplication
	{
		protected static Application Launch<T>(UIRect uiRect, UISize minSize) where T : Window
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(T),
				UIRect = uiRect,
				MinSize = minSize
			};

			application.Launch(context);

			return application;
		}
	}
}