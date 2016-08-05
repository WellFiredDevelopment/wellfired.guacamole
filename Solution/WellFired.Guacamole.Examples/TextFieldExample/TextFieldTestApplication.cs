using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.TextFieldExample
{
	[UsedImplicitly]
	public class TextFieldTestApplication
	{
		[UnityEditor.MenuItem("Window/guacamole/Test/TextField")]
		[UsedImplicitly]
		static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
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