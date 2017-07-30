using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View
{
    [UsedImplicitly]
    public class DotPeekApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/CaseStudy/DotPeek")]
        [UsedImplicitly]
        private static void OpenWindow()
        {    
            Launch<DotPeekWindow>(
                uiRect: UIRect.With(50, 50, 400, 600),
                minSize: UISize.Of(400, 600),
                title: "DotPeek");
        }
    }
}