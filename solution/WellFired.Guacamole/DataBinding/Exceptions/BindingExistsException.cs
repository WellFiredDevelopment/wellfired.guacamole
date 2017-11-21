using System;

namespace WellFired.Guacamole.DataBinding.Exceptions
{
	public class BindingExistsException : Exception
	{
		public BindingExistsException(string bindablePropertyName, string targetProperty)
		{
			BindablePropertyName = bindablePropertyName;
			TargetProperty = targetProperty;
		}

		private string BindablePropertyName { get; }
		private string TargetProperty { get; }

		public override string Message => $"Binding already exists for bindable property : {BindablePropertyName} and is bound to backstore property : {TargetProperty}, please check for duplicates";
	}
}