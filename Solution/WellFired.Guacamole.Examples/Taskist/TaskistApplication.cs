using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Taskist
{
    [UsedImplicitly]
    public class TaskistApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/CaseStudy/Taskist")]
        [UsedImplicitly]
        private static void OpenWindow()
        {
            Launch<TaskistWindow>(
                uiRect: new UIRect(400, 200, 400, 400),
                minSize: new UISize(800, 650),
                title: "Taskist");
        }
    }
}