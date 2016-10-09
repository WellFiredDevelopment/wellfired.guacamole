using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.StylingExample
{
	[UsedImplicitly]
	public class StylingExampleApplication
	{
		[MenuItem("Window/guacamole/Test/Style Test")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(StylingExampleWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 300),
				Title = "Style Test"
			};

			application.Launch(context);
		}
	}
}