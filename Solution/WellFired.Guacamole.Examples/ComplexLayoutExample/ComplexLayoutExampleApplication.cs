using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.ComplexLayoutExample
{
	[UsedImplicitly]
	public class ComplexLayoutExampleApplication
	{
		[MenuItem("Window/guacamole/Test/ComplexLayout")]
		[UsedImplicitly]
		private static void TestWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(ComplexLayoutExampleWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(200, 50),
				Title = "Complex Layout"
			};

			application.Launch(context);
		}
	}
}