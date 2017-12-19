using System;

namespace WellFired.Guacamole.DataBinding.Exceptions
{
	public class BindingExistsException : Exception
	{
		public BindingExistsException(string bindablePropertyName, string newTargetProperty, string originalTargetProperty)
		{
			BindablePropertyName = bindablePropertyName;
			NewTargetProperty = newTargetProperty;
			OriginalTargetProperty = originalTargetProperty;
		}

		private string BindablePropertyName { get; }
		private string NewTargetProperty { get; }
		private string OriginalTargetProperty { get; }

		public override string Message => $"Fail to bind to : {NewTargetProperty} . Binding already exists for bindable property : {BindablePropertyName} and is bound to backstore property : {OriginalTargetProperty}, please check for duplicates";
	}
}