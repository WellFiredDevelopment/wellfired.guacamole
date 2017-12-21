using System;

namespace WellFired.Guacamole.Diagnostics
{
	public class ConsoleLogger : ILogger
	{
		public void LogMessage(string message)
		{
			Console.WriteLine(message);
		}

		public void LogWarning(string message)
		{
			Console.WriteLine(message);
		}

		public void LogError(string message)
		{
			Console.WriteLine(message);
		}
	}
}