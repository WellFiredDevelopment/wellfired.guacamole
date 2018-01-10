using System;
using JetBrains.Annotations;

namespace WellFired.Guacamole.DataBinding
{
	public static class ConverterHelper
	{
		[PublicAPI]
		public static object ConvertTo<T>(object paramater)
		{
			return ConvertTo(typeof(T), paramater);
		}

		[PublicAPI]
		private static object ConvertTo(Type desiredType, object paramater)
		{
			if (paramater == null)
				return null;

			if (desiredType == null || desiredType.IsAssignable(paramater))
				return paramater;

			if (desiredType.IsEnum())
				return Enum.Parse(desiredType, paramater.ToString());
			if (desiredType == typeof(string))
				return paramater.ToString();
			if (desiredType == typeof(bool))
				return bool.Parse(paramater.ToString());
			if (desiredType == typeof(int))
				return int.Parse(paramater.ToString());
			if (desiredType == typeof(float))
				return float.Parse(paramater.ToString());
			if (desiredType == typeof(long))
				return long.Parse(paramater.ToString());
			if (desiredType == typeof(double))
				return double.Parse(paramater.ToString());
			if (desiredType == typeof(short))
				return short.Parse(paramater.ToString());

			return null;
		}
	}
}