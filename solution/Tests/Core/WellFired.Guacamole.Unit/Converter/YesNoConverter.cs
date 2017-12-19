using System;
using WellFired.Guacamole.DataBinding.Converter;

namespace WellFired.Guacamole.Unit.Converter
{
	public class YesNoConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			switch(value.ToString().ToLower())
			{
				case "yes":
					return true;
				case "no":
					return false;
			}
			return false;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if(value is bool aBoolean)
				return aBoolean ? "yes" : "no";
			
			return "no";
		}
	}
}