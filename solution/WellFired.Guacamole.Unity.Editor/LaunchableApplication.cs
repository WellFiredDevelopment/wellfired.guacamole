using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unity.Editor
{
	public class LaunchableApplication
	{
		protected static Application Launch<T>(UIRect uiRect, UISize minSize, string title = null) where T : Window
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(T),
				UIRect = uiRect,
				MinSize = minSize,
				Title = title
			};

			application.Launch(context);

			return application;
		}
	}
}