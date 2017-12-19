using System;

namespace WellFired.Guacamole.DataBinding.Converter
{
	public interface IValueConverter
	{
		object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
		object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);
	}
}