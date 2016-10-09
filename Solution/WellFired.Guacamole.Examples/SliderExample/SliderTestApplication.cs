using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.SliderExample
{
	[UsedImplicitly]
	public class SliderTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/Slider Test")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			Launch<SliderTestWindow>(
				uiRect: new UIRect(50, 50, 600, 200),
				minSize: new UISize(260, 30),
				title: "SliderTest");
		}
	}
}