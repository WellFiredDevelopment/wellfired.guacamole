using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.TextFieldExample
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