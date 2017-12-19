using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.AdjacentLayoutExample
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