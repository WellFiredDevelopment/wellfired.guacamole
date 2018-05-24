using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.PlaceholderTextExample
{
	public class PlaceholderTextExampleApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/Placeholder Text")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			Launch<PlaceholderTextExampleWindow>(
				uiRect: UIRect.With(50, 50, 200, 600),
				minSize: UISize.Of(200, 600),
				title: "ListViewTest");
		}
	}
}