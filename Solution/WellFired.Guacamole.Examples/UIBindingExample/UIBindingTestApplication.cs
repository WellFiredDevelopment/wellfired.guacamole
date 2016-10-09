using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.UIBindingExample
{
	// ReSharper disable once InconsistentNaming
	public static class UIBindingTestApplication
	{
		[MenuItem("Window/guacamole/Test/UIBindingTest")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Application();

			var context = new ApplicationInitializationContext
			{
				MainContent = typeof(UIBindingTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 60),
				Title = "UIBindingTest"
			};

			application.Launch(context);
		}
	}
}