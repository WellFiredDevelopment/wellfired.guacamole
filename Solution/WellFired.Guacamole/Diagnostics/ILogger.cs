namespace WellFired.Guacamole.Diagnostics
{
	public interface ILogger
	{
		void LogMessage(string message);
		void LogWarning(string message);
		void LogError(string message);
	}
}