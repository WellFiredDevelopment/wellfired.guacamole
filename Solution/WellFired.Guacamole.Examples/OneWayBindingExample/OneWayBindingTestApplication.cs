using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.OneWayBindingExample
{
	[UsedImplicitly]
	public class OneWayBindingTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/OneWayBindingTest")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<OneWayBindingTestWindow>(
				uiRect: new UIRect(50, 50, 600, 200),
				minSize: new UISize(200, 50),
				title: "OneWayBindingTest");
		}
	}
}