using System;
using System.Globalization;
using WellFired.Guacamole.DataBinding.Converter;

namespace WellFired.Guacamole.Unit.Converter
{
	public class PlurialConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var stringValue = value.ToString();
			if (stringValue.EndsWith("s"))
				return stringValue.Substring(0, stringValue.Length - 1);
			
			return stringValue + "s";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var stringValue = value.ToString();
			if (stringValue.EndsWith("s"))
				return stringValue.Substring(0, stringValue.Length - 1);
			
			return stringValue + "s";
		}
	}
}