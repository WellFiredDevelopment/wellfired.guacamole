using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.LabelExample
{
    [UsedImplicitly]
    public class LabelTestApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/Test/Label Test")]
        [UsedImplicitly]
        private static void OpenWindow()
        {
            Launch<LabelTestWindow>(
                uiRect: UIRect.With(50, 50, 600, 200),
                minSize: UISize.Of(260, 30),
                title: "LabelTest");
        }
    }
}