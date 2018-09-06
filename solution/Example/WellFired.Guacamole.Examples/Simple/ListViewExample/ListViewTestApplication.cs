using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
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
                uiRect: UIRect.With(50, 50, 600, 600),
                minSize: UISize.Of(200, 600),
                title: "ListViewTest");
        }
    }
}