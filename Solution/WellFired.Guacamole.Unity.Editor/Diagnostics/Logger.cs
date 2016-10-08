using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.Unity.Editor.Diagnostics
{
    public class Logger : ILogger
    {
        public static ILogger UnityLogger { get; } = new Logger();

        public void LogMessage(string message)
        {
            UnityEngine.Debug.Log(message);
        }

        public void LogWarning(string message)
        {
            UnityEngine.Debug.LogWarning(message);
        }

        public void LogError(string message)
        {
            UnityEngine.Debug.LogError(message);
        }
    }
}