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
                uiRect: UIRect.With(400, 200, 800, 650),
                minSize: UISize.Of(800, 650),
                title: "Taskist");
        }
    }
}