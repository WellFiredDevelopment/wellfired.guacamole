using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.TwoWayBindingExample
{
	[UsedImplicitly]
	public class TwoWayBindingTestApplication
	{
		[MenuItem("Window/guacamole/Test/TwoWayBindingTest")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(TwoWayBindingTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "TwoWayBindingTest"
			};

			application.Launch<TwoWayBindingTestModel>(context);
		}
	}
}