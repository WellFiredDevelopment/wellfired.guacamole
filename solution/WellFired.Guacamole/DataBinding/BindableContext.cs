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
		/// This is the property on the destination side.
		/// </summary>
		public BindableProperty BindableProperty;

		public string SourcePropertyName
		{
			get => _sourcePropertyName;
			set
			{
				_sourcePropertyName = value;
				ConfigureSet();
			}
		}

		/// <summary>
		/// This is the current value of the destination property.
		/// </summary>
		public object Value { private set; get; }

		/// <summary>
		/// This is the source object destination is bound to.
		/// </summary>
		public INotifyPropertyChanged SourceObject
		{
			private get { return _sourceObject; }
			set
			{
				_sourceObject = value;
				ConfigureSet();
			}
		}

		/// <summary>
		/// This describe in which way the source and destination are bound. If it is not specified, the bindable property default
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
		
		private INotifyPropertyChanged _sourceObject;
		private MethodInfo _srcPropertyGetMethod;
		private PropertyInfo _srcPropertyInfo;
		private MethodInfo _srcPropertySetMethod;
		private string _sourcePropertyName;

		private void ConfigureSet()
		{
			if (SourceObject == null || BindableProperty == null || SourcePropertyName == null)
			{
				_srcPropertyInfo = null;
				_srcPropertySetMethod = null;
			}
			else
			{
				var type = SourceObject.GetType();
				_srcPropertyInfo = type.GetProperty(SourcePropertyName, BindingFlags.Public | BindingFlags.Instance);

				if (_srcPropertyInfo == null)
					throw new PropertyNotFoundException(BindableProperty.PropertyName, type.Name, SourcePropertyName);
				_srcPropertySetMethod = _srcPropertyInfo.GetSetMethod();
				_srcPropertyGetMethod = _srcPropertyInfo.GetGetMethod();
			}
		}

		private object GetValue()
		{
			if (_srcPropertyGetMethod == null) 
				return Value;
			
			var value = _srcPropertyGetMethod?.Invoke(SourceObject, null);
			var converter = InstancedConverter ?? DefaultConverter;
			return converter.ConvertBack(value, BindableProperty.PropertyType, null, CultureInfo.CurrentCulture);

		}

		/// <summary>
		/// This is called when the value on the destination was changed (In a VMMV context it would be the View).
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
				
				Value = DefaultConverter.Convert(value, BindableProperty.PropertyType, null, CultureInfo.CurrentCulture);		
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
	
				if (_srcPropertySetMethod == null)
					return true; // We return true here because we've got far enough to set our Value, even if we don't have a _propertySetMethod. Which means we're probably not bound to anything
	
				var converted = InstancedConverter != null ? InstancedConverter.Convert(value, _srcPropertyInfo.PropertyType, null, CultureInfo.CurrentCulture) : value;
				_srcPropertySetMethod.Invoke(SourceObject, new[] {converted});
				return true;
			}
			catch (Exception e)
			{
				throw new SetValueFromDestException(SourceObject, BindableProperty.PropertyName, _sourcePropertyName, value, e);
			}
		}
		
		/// <summary>
		/// This initialize the value of the source. It is called before binding occurs.
		/// </summary>
		/// <param name="value"></param>
		public void InitializeSourceValue(object value)
		{
			Value = value;
			SetValueFromSource();
		}

		/// <summary>
		/// This is called when the value on the source was changed (In a VMMV context it would be the VM).
		/// </summary>
		/// <exception cref="SetValueFromSourceException"></exception>
		/// <returns></returns>
		public bool SetValueFromSource()
		{
			object value = null;
			
			try
			{
				value = GetValue();
				
				if (Equals(Value, value))
					return false;

				Value = value;

				return true;
			}
			catch (Exception e)
			{
				throw new SetValueFromSourceException(SourceObject, BindableProperty.PropertyName, SourcePropertyName, value, e);
			}
		}
	}
}