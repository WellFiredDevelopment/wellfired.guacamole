using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.UIBindingExample
{
    // ReSharper disable once InconsistentNaming
	public static class UIBindingTestApplication
	{
		[UnityEditor.MenuItem("Window/guacamole/Test/UIBindingTest")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
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