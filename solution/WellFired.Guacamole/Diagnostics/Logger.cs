using System.Collections.Generic;
using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Diagnostics
{
	public static class Logger
	{
		private static readonly List<ILogger> Loggers = new List<ILogger>();

		[PublicAPI]
		public static void RegisterLogger(ILogger logger)
		{
			if (!Loggers.Contains(logger))
				Loggers.Add(logger);
		}

		[PublicAPI]
		public static void UnregisterLogger(ILogger logger)
		{
			Loggers.Remove(logger);
		}

		[PublicAPI]
		public static void LogMessage(string message)
		{
			Loggers.ForEach(logger => logger.LogMessage(message));
		}

		[PublicAPI]
		public static void LogWarning(string message)
		{
			Loggers.ForEach(logger => logger.LogWarning(message));
		}

		[PublicAPI]
		public static void LogError(string message)
		{
			Loggers.ForEach(logger => logger.LogError(message));
		}
	}
}