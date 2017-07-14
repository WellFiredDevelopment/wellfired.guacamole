using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.StylingExample
{
	[UsedImplicitly]
	public class StylingExampleApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/Style Test")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<StylingExampleWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 30),
				title: "Style Test");
		}
	}
}