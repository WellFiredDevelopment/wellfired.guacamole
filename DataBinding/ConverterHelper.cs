using System;

namespace WellFired.Guacamole.Databinding
{
    public static class ConverterHelper
    {
        public static object ConvertTo<T>(object paramater)
        {
            return ConvertTo(typeof(T), paramater);
        }

        public static object ConvertTo(Type desiredType, object paramater)
        {
            if(paramater == null)
                return null;

            if(desiredType == null || desiredType.IsAssignable(paramater))
                return paramater;

            if(desiredType.IsEnum())
                return Enum.Parse(desiredType, paramater.ToString());
            else if(desiredType == typeof(string))
                return paramater.ToString();
            else if(desiredType == typeof(bool))
                return bool.Parse(paramater.ToString());
            else if(desiredType == typeof(int))
                return int.Parse(paramater.ToString());
            else if(desiredType == typeof(float))
                return float.Parse(paramater.ToString());
            else if(desiredType == typeof(long))
                return long.Parse(paramater.ToString());
            else if(desiredType == typeof(double))
                return double.Parse(paramater.ToString());
            else if(desiredType == typeof(short))
                return short.Parse(paramater.ToString());

            return null;
        }
    }
}