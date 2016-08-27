using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.StylingExample
{
	[UsedImplicitly]
	public class StylingExampleApplication
    {
		[UnityEditor.MenuItem("Window/guacamole/Test/Style Test")]
		[UsedImplicitly]
		static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
			{
				MainContent = typeof(StylingExampleWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "ButtonTest"
			};

			application.Launch(context);
		}
	}
}