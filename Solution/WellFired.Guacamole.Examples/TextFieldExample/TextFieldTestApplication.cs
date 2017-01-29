using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.TextFieldExample
{
	[UsedImplicitly]
	public class TextFieldTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/TextField")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			Launch<TextFieldTestWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 30),
				title: "TestField Test");
		}
	}
}