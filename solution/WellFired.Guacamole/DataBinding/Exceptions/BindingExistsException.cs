using System;

namespace WellFired.Guacamole.DataBinding.Exceptions
{
	public class BindingExistsException : Exception
	{
		public BindingExistsException(string bindablePropertyName, string newSourceProperty, string originalSourceProperty)
		{
			BindablePropertyName = bindablePropertyName;
			NewSourceProperty = newSourceProperty;
			OriginalSourceProperty = originalSourceProperty;
		}

		private string BindablePropertyName { get; }
		private string NewSourceProperty { get; }
		private string OriginalSourceProperty { get; }

		public override string Message => $"Failed binding to <{NewSourceProperty}>. Binding already exists for bindable property <{BindablePropertyName}> and is bound to backstore property <{OriginalSourceProperty}>, please check for duplicates";
	}
}