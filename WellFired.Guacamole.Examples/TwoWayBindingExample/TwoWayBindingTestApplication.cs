using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.TwoWayBindingExample
{
	[UsedImplicitly]
	public class TwoWayBindingTestApplication
	{
		[UnityEditor.MenuItem("Window/guacamole/Test/TwoWayBindingTest")]
		[UsedImplicitly]
		static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
			{
				MainContent = typeof(TwoWayBindingTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "TwoWayBindingTest"
			};

			application.Launch(context);
		}
	}
}