using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.OneWayBindingExample
{
	[UsedImplicitly]
	public class OneWayBindingTestApplication
	{
		[MenuItem("Window/guacamole/Test/OneWayBindingTest")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(OneWayBindingTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "OneWayBindingTest"
			};

			application.Launch<OneWayBindingTestModel>(context);
		}
	}
}