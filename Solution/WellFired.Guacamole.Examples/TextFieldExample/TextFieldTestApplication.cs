using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.TextFieldExample
{
	[UsedImplicitly]
	public class TextFieldTestApplication
	{
		[MenuItem("Window/guacamole/Test/TextField")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(TextFieldTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "TestFieldTest"
			};

			application.Launch(context);
		}
	}
}