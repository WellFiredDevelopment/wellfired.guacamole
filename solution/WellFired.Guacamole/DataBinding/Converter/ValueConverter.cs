using System;
using System.ComponentModel;
using System.Globalization;

namespace WellFired.Guacamole.DataBinding.Converter
{
	public class ValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertInternal(value, targetType);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertInternal(value, targetType);
		}

		private static object ConvertInternal(object value, Type targetType)
		{
			if (targetType == typeof(string))
				return value?.ToString();

			if (targetType == typeof(double) && value is int i)
				return i.ConvertToDouble();

			if (value == null)
			{
				var canBeNull = !targetType.IsValueType || Nullable.GetUnderlyingType(targetType) != null;
				if (canBeNull)
					return null;
				
				throw new SystemException("Converting null to a non nullable type");
			}

			if (value.GetType() == targetType)
				return value;

			if (targetType.IsInstanceOfType(value))
				return value;

			var converter = TypeDescriptor.GetConverter(targetType);
			if (converter.CanConvertFrom(value.GetType()))
				return converter.ConvertFrom(value);

			throw new SystemException($"Cannot convert {value} of type {value.GetType()} to {targetType}");
		}
	}
}