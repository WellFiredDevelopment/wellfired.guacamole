using System;

namespace WellFired.Guacamole.DataBinding.Converter
{
	public static class Extensions
	{
		public static bool TryConvertToInt32(this decimal decimalValue, out int intValue)
		{
			intValue = 0;
			
			if (decimalValue < int.MinValue || decimalValue > int.MaxValue) 
				return false;
			
			intValue = Convert.ToInt32(decimalValue);
			return true;
		}
		
		public static decimal ConvertToDecimal(this int intValue)
		{
			return Convert.ToDecimal(intValue);
		}
		
		public static double ConvertToDouble(this int intValue)
		{
			return Convert.ToDouble(intValue);
		}
	}
}