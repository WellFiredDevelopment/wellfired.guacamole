using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.SliderExample
{
	[UsedImplicitly]
	public class SliderTestApplication
	{
		[MenuItem("Window/guacamole/Test/Slider Test")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(SliderTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "SliderTest"
			};

			application.Launch(context);
		}
	}
}