using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.DotPeekRefresh
{
    [UsedImplicitly]
    public class DotPeekRefreshApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/CaseStudy/DotPeekRefresh")]
        [UsedImplicitly]
        private static void OpenWindow()
        {    
            Launch<DotPeekRefreshWindow>(
                uiRect: UIRect.With(50, 50, 400, 500),
                minSize: UISize.Of(400, 500),
                title: "DotPeekRefresh");
        }
    }
}