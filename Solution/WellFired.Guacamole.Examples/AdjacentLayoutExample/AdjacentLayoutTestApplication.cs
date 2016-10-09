using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.AdjacentLayoutExample
{
	[UsedImplicitly]
	public class AdjacentLayoutTestApplication
	{
		[MenuItem("Window/guacamole/Test/AdjacentLayoutTest")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(AdjacentLayoutTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 230),
				Title = "AdjacentLayoutTest"
			};

			application.Launch(context);
		}
	}
}