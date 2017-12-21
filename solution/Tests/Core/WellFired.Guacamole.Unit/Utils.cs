using System;
using System.IO;
using System.Reflection;

namespace WellFired.Guacamole.Unit
{
	public static class Utils
	{
		public static string GetTestDllRepository()
		{
			var dllPath = new Uri(Assembly.GetExecutingAssembly()
				.CodeBase).AbsolutePath;
			return Path.GetDirectoryName(dllPath);
		}
	}
}