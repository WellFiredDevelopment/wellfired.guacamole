using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.Examples.Intermediate.KeyValueListViewExample.ViewModel;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Intermediate.KeyValueListViewExample
{
	public class KeyValueListViewTestApplication : LaunchableApplication
	{
		[MenuItem("Window/guacamole/Intermediate/KeyValue List View Test")]
		[UsedImplicitly]
		private static void OpenWindow()
		{
			Launch<KeyValueListViewTestWindow, SettingsViewModel>(
				uiRect: UIRect.With(50, 50, 200, 600),
				minSize: UISize.Of(200, 600),
				title: "KeyValueListViewTest");
		}
	}
}