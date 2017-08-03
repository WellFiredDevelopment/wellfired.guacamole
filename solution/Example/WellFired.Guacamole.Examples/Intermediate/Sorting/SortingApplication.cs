using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Intermediate.Sorting
{
    [UsedImplicitly]
    public class SortingApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/Intermediate/Sorting")]
        [UsedImplicitly]
        private static void OpenWindow()
        {    
            Launch<SortingWindow>(
                uiRect: UIRect.With(50, 50, 600, 600),
                minSize: UISize.Of(600, 600),
                title: "Sorting");
        }
    }
}