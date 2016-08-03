using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.AdjacentLayoutExample
{
	[UsedImplicitly]
	public class AdjacentLayoutTestApplication
	{
		[MenuItem("Window/guacamole/Test/AdjacentLayoutTest")]
		[UsedImplicitly]
		static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
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