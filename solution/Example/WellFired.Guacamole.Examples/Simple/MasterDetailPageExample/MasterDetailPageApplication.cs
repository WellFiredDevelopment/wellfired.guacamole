using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.MasterDetailPageExample
{
    public class MasterDetailPageApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/Test/Master Detail Page Test")]
        [UsedImplicitly]
        private static void OpenWindow()
        {
            Launch<MasterDetailPageWindow>(
                uiRect: UIRect.With(50, 50, 800, 600),
                minSize: UISize.Of(800, 600),
                title: "Master Detail Example");
        }
    }
}