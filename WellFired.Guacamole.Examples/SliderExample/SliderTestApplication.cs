namespace WellFired.Guacamole.Examples.TextField
{
	public class SliderTestApplication
	{
		[UnityEditor.MenuItem("Window/guacamole/Test/Slider Test")]
		static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
			{
				MainContent = typeof(SliderTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "SliderTest"
			};

			application.Launch(context);
		}
	}
}