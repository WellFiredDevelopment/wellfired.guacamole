namespace WellFired.Guacamole.Examples.ComplexLayoutExample
{
	public class ComplexLayoutExampleApplication
	{
		[UnityEditor.MenuItem ("Window/guacamole/Test/ComplexLayout")]
		static void TestWindow () 
		{
			var application = new Unity.Editor.Application();

			var context = new Unity.Editor.ApplicationInitializationContext
			{
				MainContent = typeof (ComplexLayoutExampleWindow),
				UIRect = new UIRect(50, 50, 600, 200),
				MinSize = new UISize(200, 50),
				Title = "Complex Layout"
			};

			application.Launch(context);
		}
	}
}