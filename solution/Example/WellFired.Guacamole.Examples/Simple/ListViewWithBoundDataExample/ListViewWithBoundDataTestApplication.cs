using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.ListViewWithBoundDataExample
{
    public class ListViewWithBoundDataTestApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/Test/List View With Bound Data Test")]
        [UsedImplicitly]
        private static void OpenWindow()
        {
            Launch<ListViewWithBoundDataTestWindow>(
                uiRect: UIRect.With(50, 50, 200, 600),
                minSize: UISize.Of(200, 600),
                title: "ListViewWithBoundDataTest");
        }
    }
}