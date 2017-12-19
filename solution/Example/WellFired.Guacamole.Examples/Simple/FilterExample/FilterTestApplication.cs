using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.FilterExample
{
	[UsedImplicitly]
	public class FilterTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/Filter Test")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<FilterTestWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 30),
				title: "FilterTest");
		}
	}
}