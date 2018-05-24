using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.ToggleViewExample
{
	[UsedImplicitly]
	public class ToggleViewTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/ToggleView Test")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<ToggleViewTestWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 30),
				title: "ToggleViewTest");
		}
	}
}