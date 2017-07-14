using UnityEngine;
using ILogger = WellFired.Guacamole.Diagnostics.ILogger;

namespace WellFired.Guacamole.Unity.Editor.Diagnostics
{
	public class Logger : ILogger
	{
		public void LogMessage(string message)
		{
			Debug.Log(message);
		}

		public void LogWarning(string message)
		{
			Debug.LogWarning(message);
		}

		public void LogError(string message)
		{
			Debug.LogError(message);
		}
	}
}