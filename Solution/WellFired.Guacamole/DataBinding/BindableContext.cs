using System;
using System.ComponentModel;
using System.Reflection;

namespace WellFired.Guacamole.DataBinding
{
	public class BindableContext
	{
		public BindableProperty Property;
		private PropertyInfo _propertyInfo;
		private MethodInfo _propertySetMethod;
		private MethodInfo _propertyGetMethod;
		private INotifyPropertyChanged _bindableObject;
		private object _value;
		private string _targetProperty;

		public string TargetProperty 
		{
		    private get { return _targetProperty; }
			set {
				_targetProperty = value;
				ConfigureSet();
			}
		}

		public object Value
		{
			set
			{
				if (Equals(_value, value))
					return;

				_value = BindableContextConverter.From(value, Property);

				switch (Property.BindingMode)
				{
					case BindingMode.OneWay:
						return;
					case BindingMode.TwoWay:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}

			    _propertySetMethod?.Invoke(Object, new[] {value});
			}
			get { return _value; }
		}

		public INotifyPropertyChanged Object
		{
		    private get { return _bindableObject; }
			set
			{
				_bindableObject = value;
				ConfigureSet();
			}
		}

		private void ConfigureSet()
		{
			if (Object == null || Property == null || TargetProperty == null)
			{
				_propertyInfo = null;
				_propertySetMethod = null;
			}
			else
			{
				var type = Object.GetType();
				_propertyInfo = type.GetProperty(TargetProperty, BindingFlags.Public | BindingFlags.Instance);
				_propertySetMethod = _propertyInfo.GetSetMethod();
				_propertyGetMethod = _propertyInfo.GetGetMethod();
			}
		}

		public object GetValue()
		{
			return _propertyGetMethod?.Invoke(Object, null);
		}
	}

	public static class BindableContextConverter
	{
		public static object From(object value, BindableProperty property)
		{
			if(property.PropertyType == typeof(string))
				return value?.ToString();

			if(value.GetType() == property.PropertyType)
				return value;

            if (property.PropertyType.IsInstanceOfType(value))
                return value;

			var converter = TypeDescriptor.GetConverter(property.PropertyType);
			if (converter.CanConvertFrom(value.GetType()))
				return converter.ConvertFrom(value);

			throw new SystemException($"Cannot convert {value} to {property.PropertyType}");
		}
	}
}