using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.TabbedPageExample
{
	[UsedImplicitly]
	public class TabbedPageApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/TabbedPageTest")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<TabbedPageWindow>(
				uiRect: UIRect.With(50, 50, 600, 600),
				minSize: UISize.Of(600, 600),
				title: "TabbedPageWindow");
		}
	}
}