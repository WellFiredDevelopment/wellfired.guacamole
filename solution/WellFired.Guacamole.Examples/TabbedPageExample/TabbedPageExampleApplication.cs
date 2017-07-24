using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.TabbedPageExample
{
	[UsedImplicitly]
	public class TabbedPageExampleApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/TabbedPage Test")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<TabbedPageExampleWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 30),
				title: "Style Test");
		}
	}
}