using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.TwoWayBindingExample
{
	[UsedImplicitly]
	public class TwoWayBindingTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/TwoWayBindingTest")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			Launch<TwoWayBindingTestWindow>(
				uiRect: new UIRect(50, 50, 600, 200),
				minSize: new UISize(260, 30),
				title: "TwoWayBindingTest Test");
		}
	}
}