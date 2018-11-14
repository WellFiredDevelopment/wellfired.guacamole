using System;
using System.ComponentModel;
using WellFired.Guacamole.Exceptions;

namespace WellFired.Guacamole.DataBinding.Exceptions
{
	public class SetValueFromDestException : GuacamoleUserFacingException
	{
		private readonly INotifyPropertyChanged _bindableObject;
		private readonly string _propertyPropertyName;
		private readonly string _targetProperty;
		private readonly object _value;
		private readonly Exception _exception;

		public SetValueFromDestException(INotifyPropertyChanged bindableObject, string propertyPropertyName, string targetProperty, object value, Exception exception)
		{
			_bindableObject = bindableObject;
			_propertyPropertyName = propertyPropertyName;
			_targetProperty = targetProperty;
			_value = value;
			_exception = exception;
		}

		public override string UserFacingError()
		{
			var exception = _exception;
			if (_exception.InnerException != null)
			{
				exception = _exception.InnerException;
			}
			
			return $"An error occured when trying to assign the value {_value} of the destination property {_propertyPropertyName} to the property {_targetProperty} of " +
			       $"the bindable object of type {_bindableObject.GetType()}. Details : \n{exception.Message}\n{exception.StackTrace}";
		}
	}
}