using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.ComplexLayoutExample
{
	[UsedImplicitly]
	public class ComplexLayoutExampleApplication: LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/ComplexLayout")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<ComplexLayoutExampleWindow>(
				uiRect: new UIRect(50, 50, 600, 200),
				minSize: new UISize(200, 50),
				title: "Complex UpdateContextIfNeeded");
		}
	}
}