using System.Diagnostics;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Diagnostics
{
	[PublicAPI]
	public class DefaultLogger : ILogger
	{
		public void LogMessage(string message)
		{
			Debug.Write(message);
		}

		public void LogWarning(string message)
		{
			Debug.Write(message);
		}

		public void LogError(string message)
		{
			Debug.Write(message);
		}
	}
}