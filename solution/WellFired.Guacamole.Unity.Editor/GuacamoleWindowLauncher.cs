using System;
using UnityEngine;

namespace WellFired.Guacamole.Unity.Editor
{
    public static class GuacamoleWindowLauncher
    {
        internal static GuacamoleWindow LaunchWindow(Type mainContent)
        {
            var foundWindows = Resources.FindObjectsOfTypeAll(typeof(GuacamoleWindow)) as GuacamoleWindow[];

            if (foundWindows != null)
            {
                foreach (var window in foundWindows)
                {
                    if (!window.MatchesMainContent(mainContent))
                        continue;

                    if (!window.AllowMultiple)
                        return window;
                }
            }

            var instance = ScriptableObject.CreateInstance<GuacamoleWindow>();
            instance.Show();
            return instance;
        }
    }
}