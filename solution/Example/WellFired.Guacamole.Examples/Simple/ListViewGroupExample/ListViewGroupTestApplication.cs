﻿using JetBrains.Annotations;
using UnityEditor;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Unity.Editor;

namespace WellFired.Guacamole.Examples.Simple.ListViewGroupExample
{
    public class ListViewGroupTestApplication : LaunchableApplication
    {
        [MenuItem("Window/guacamole/Test/List View Group Test")]
        [UsedImplicitly]
        private static void OpenWindow()
        {
            Launch<ListViewGroupTestWindow>(
                uiRect: UIRect.With(50, 50, 200, 600),
                minSize: UISize.Of(200, 600),
                title: "ListViewTest");
        }
    }
}