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
		private static readonly IValueConverter DefaultConverter = new ValueConverter(); 
		
		/// <summary>
		/// View property which is bound to the VM.
		/// </summary>
		public BindableProperty Property;

		/// <summary>
		/// This is the property on the VM
		/// </summary>
		public string TargetProperty
		{
			get => _targetProperty;
			set
			{
				_targetProperty = value;
				ConfigureSet();
			}
		}

		/// <summary>
		/// This is the value set on the view or sent to the VM after being converted.
		/// </summary>
		public object Value { private set; get; }

		/// <summary>
		/// Object is the backing store (VM)
		/// </summary>
		public INotifyPropertyChanged Object
		{
			private get { return _bindableObject; }
			set
			{
				_bindableObject = value;
				ConfigureSet();
			}
		}

		/// <summary>
		/// This describe in which way the VM and View are bound. If it is not specified, the bindable property default
		/// <see cref="BindingMode"/> is used.
		/// </summary>
		public BindingMode InstancedBindingMode
		{
			get;
			set;
		}

		/// <summary>
		/// This can be specify to apply a custom conversion to the value. If not specified, the default <see cref="ValueConverter"/>
		/// is used.
		/// </summary>
		public IValueConverter InstancedConverter
		{
			get; 
			set;
		}
		
		private INotifyPropertyChanged _bindableObject;
		private MethodInfo _propertyGetMethod;
		private PropertyInfo _propertyInfo;
		private MethodInfo _propertySetMethod;
		private string _targetProperty;

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

				if (_propertyInfo == null)
					throw new PropertyNotFoundException(Property.PropertyName, type.Name, TargetProperty);
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
				
				Value = DefaultConverter.Convert(value, Property.PropertyType, null, CultureInfo.CurrentCulture);		
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
			catch (Exception e)
			{
				throw new SetValueFromDestException(_bindableObject, Property.PropertyName, _targetProperty, value, e);
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
				var converter = InstancedConverter ?? DefaultConverter;
				var convertedValue = converter.ConvertBack(value, Property.PropertyType, null, CultureInfo.CurrentCulture);
				if (Equals(Value, convertedValue))
					return false;

				Value = convertedValue;

				_propertySetMethod?.Invoke(Object, new[] {value});
				return true;
			}
			catch (Exception e)
			{
				throw new SetValueFromSourceException(_bindableObject, Property.PropertyName, _targetProperty, value, e);
			}
		}
	}
}