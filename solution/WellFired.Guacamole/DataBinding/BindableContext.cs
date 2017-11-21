using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using WellFired.Guacamole.DataBinding.Converter;
using WellFired.Guacamole.DataBinding.Exceptions;

namespace WellFired.Guacamole.DataBinding
{
	public class BindableContext
	{
		private static IValueConverter _defaultConverter = new ValueConverter(); 
		
		private INotifyPropertyChanged _bindableObject;
		private MethodInfo _propertyGetMethod;
		private PropertyInfo _propertyInfo;
		private MethodInfo _propertySetMethod;
		private string _targetProperty;
		public BindableProperty Property;

		public string TargetProperty
		{
			private get { return _targetProperty; }
			set
			{
				_targetProperty = value;
				ConfigureSet();
			}
		}

		public object Value { private set; get; }

		public INotifyPropertyChanged Object
		{
			private get { return _bindableObject; }
			set
			{
				_bindableObject = value;
				ConfigureSet();
			}
		}

		public BindingMode InstancedBindingMode
		{
			get; 
			set;
		}

		public IValueConverter InstancedConverter
		{
			get; 
			set;
		}

		private void ConfigureSet()
		{
			if ((Object == null) || (Property == null) || (TargetProperty == null))
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
			return _propertyGetMethod == null ? Value : _propertyGetMethod?.Invoke(Object, null);
		}

		/// <summary>
		/// In this context, dest would typically be the UI (View)
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public bool SetValueFromDest(object value)
		{
			try
			{
				if (Equals(Value, value))
					return false;
				
				Value = _defaultConverter.Convert(value, Property.PropertyType, null, CultureInfo.CurrentCulture);;				
				switch (InstancedBindingMode)
				{
					case BindingMode.OneWay:
					case BindingMode.ReadOnly:
						return false;
					case BindingMode.TwoWay:
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
	
				if (_propertySetMethod == null)
					return true; // We return true here because we've got far enough to set our Value, even if we don't have a _propertySetMethod. Which means we're probably not bound to anything
	
				var converted = InstancedConverter != null ? InstancedConverter.Convert(value, _propertyInfo.PropertyType, null, CultureInfo.CurrentCulture) : value;
				_propertySetMethod.Invoke(Object, new[] {converted});
				return true;
			}
			catch (Exception)
			{
				throw new SetValueFromDestException(_bindableObject, Property.PropertyName, _targetProperty, value);
			}
		}

		/// <summary>
		/// In this context, source would typically be the backing store (VM)
		/// </summary>
		/// <param name="value"></param>
		/// <exception cref="SetValueFromSourceException"></exception>
		/// <returns></returns>
		public bool SetValueFromSource(object value)
		{
			try
			{
				if (Equals(Value, value))
					return false;

				var converter = InstancedConverter ?? _defaultConverter;

				Value = converter.ConvertBack(value, Property.PropertyType, null, CultureInfo.CurrentCulture);

				_propertySetMethod?.Invoke(Object, new[] {value});
				return true;
			}
			catch (Exception)
			{
				throw new SetValueFromSourceException(_bindableObject, Property.PropertyName, _targetProperty, value);
			}
		}
	}
}