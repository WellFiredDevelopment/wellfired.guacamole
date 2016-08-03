using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.OneWayBindingExample
{
	[UsedImplicitly]
	public class OneWayBindingTestApplication
	{
		[MenuItem("Window/guacamole/Test/OneWayBindingTest")]
		[UsedImplicitly]
		static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
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