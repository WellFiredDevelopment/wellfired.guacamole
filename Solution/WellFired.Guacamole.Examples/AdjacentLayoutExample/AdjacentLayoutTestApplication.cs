using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.AdjacentLayoutExample
{
	[UsedImplicitly]
	public class AdjacentLayoutTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/AdjacentLayoutTest")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<AdjacentLayoutTestWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 230),
				title: "AdjacentLayoutTest");
		}
	}
}