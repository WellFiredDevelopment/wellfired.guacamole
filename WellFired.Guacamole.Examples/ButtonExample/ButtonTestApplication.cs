namespace WellFired.Guacamole.Examples.TextField
{
	public class ButtonTestApplication
	{
		[UnityEditor.MenuItem("Window/guacamole/Test/Button Test")]
		static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
			{
				MainContent = typeof(ButtonTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "ButtonTest"
			};

			application.Launch(context);
		}
	}
}