namespace WellFired.Guacamole.Examples.TwoWayBinding
{
	public class OneWayBindingTestApplication
	{
		[UnityEditor.MenuItem("Window/guacamole/Test/OneWayBindingTest")]
		static void OpenWindow()
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
			{
				MainContent = typeof(OneWayBindingTestWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(260, 30),
				Title = "OneWayBindingTest"
			};

			application.Launch(context);
		}
	}
}