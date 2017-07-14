using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.ListViewExample
{
    public class ListViewTestApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/Test/List View Test")]
        [UsedImplicitly]
        private static void OpenWindow()
        {
            Launch<ListViewTestWindow>(
                uiRect: UIRect.With(50, 50, 200, 600),
                minSize: UISize.Of(200, 600),
                title: "ListViewTest");
        }
    }
}