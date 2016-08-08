namespace WellFired.Guacamole.Diagnostics
{
    public class DefaultLogger : ILogger
    {
        public void LogMessage(string message)
        {
            System.Diagnostics.Debug.Write(message);
        }

        public void LogWarning(string message)
        {
            System.Diagnostics.Debug.Write(message);
        }

        public void LogError(string message)
        {
            System.Diagnostics.Debug.Write(message);
        }
    }
}