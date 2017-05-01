using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.LabelExample
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