using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView
{
    [UsedImplicitly]
    public class DotPeekApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/CaseStudy/DotPeekWithListView")]
        [UsedImplicitly]
        private static void OpenWindow()
        {    
            Launch<DotPeekWindow>(
                uiRect: UIRect.With(50, 50, 600, 600),
                minSize: UISize.Of(600, 600),
                title: "DotPeekWithListView");
        }
    }
}