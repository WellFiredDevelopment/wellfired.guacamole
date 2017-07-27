using UnityEditor;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.CaseStudy.RedditBrowser
{
    [UsedImplicitly]
    public class RedditBrowserApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/CaseStudy/Reddit Browser")]
        [UsedImplicitly]
        private static void OpenWindow()
        {    
            Launch<RedditBrowserWindow>(
                uiRect: UIRect.With(50, 50, 600, 600),
                minSize: UISize.Of(600, 600),
                title: "Reddit Browser");
        }
    }
}