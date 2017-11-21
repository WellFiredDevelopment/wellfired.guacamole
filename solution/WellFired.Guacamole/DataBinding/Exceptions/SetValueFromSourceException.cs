﻿using System.ComponentModel;
using WellFired.Guacamole.Exceptions;

namespace WellFired.Guacamole.DataBinding.Exceptions
{
	public class SetValueFromSourceException : GuacamoleUserFacingException
	{
		private readonly INotifyPropertyChanged _bindableObject;
		private readonly string _propertyPropertyName;
		private readonly string _targetProperty;
		private readonly object _value;

		public SetValueFromSourceException(INotifyPropertyChanged bindableObject, string propertyPropertyName, string targetProperty, object value)
		{
			_bindableObject = bindableObject;
			_propertyPropertyName = propertyPropertyName;
			_targetProperty = targetProperty;
			_value = value;
		}

		public override string UserFacingError()
		{
			return $"An error occured when trying to assign the value {_value} of the backstore property {_targetProperty} to the property {_propertyPropertyName} of " +
			       $"the bindable object of type {_bindableObject.GetType()}. Details : \n{Message}\n{StackTrace}";
		}
	}
}