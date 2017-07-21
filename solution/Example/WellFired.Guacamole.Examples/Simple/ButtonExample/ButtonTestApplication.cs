using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.ButtonExample
{
	[UsedImplicitly]
	public class ButtonTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/Button Test")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<ButtonTestWindow>(
				uiRect: UIRect.With(50, 50, 600, 200),
				minSize: UISize.Of(260, 30),
				title: "ButtonTest");
		}
	}
}