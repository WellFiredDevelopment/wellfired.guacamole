using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.ImageViewExample
{
	[UsedImplicitly]
	public class ImageViewWindowApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Test/Image View Test")]
		[UsedImplicitly]
		private static void Launch()
		{
			Launch<ImageViewWindow>(
				uiRect: UIRect.With(50, 50, 400, 400),
				minSize: UISize.Of(400, 400),
				title: "ImageView",
				applicationName: "ImageViewTest");
		}
	}
}