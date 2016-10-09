using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.ButtonExample
{
	[UsedImplicitly]
	public class ButtonTestApplication
	{
		[MenuItem("Window/guacamole/Test/Button Test")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(ButtonTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "ButtonTest"
			};

			application.Launch(context);
		}
	}
}