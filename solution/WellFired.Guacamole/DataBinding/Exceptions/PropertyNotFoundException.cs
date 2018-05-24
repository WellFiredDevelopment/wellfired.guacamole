using System;

namespace WellFired.Guacamole.DataBinding.Exceptions
{
	public class PropertyNotFoundException : Exception
	{
		public PropertyNotFoundException(string bindablePropertyName, string backstoreType, string unexistingBackstoreProperty)
		{
			BindablePropertyName = bindablePropertyName;
			BackstoreType = backstoreType;
			UnexistingBackstoreProperty = unexistingBackstoreProperty;
		}

		private string BindablePropertyName { get; }
		private string BackstoreType { get; }
		private string UnexistingBackstoreProperty { get; }

		public override string Message => $"<{BackstoreType}> does not have the property <{UnexistingBackstoreProperty}>. <{BindablePropertyName}> cannot be bound to it.";
	}
}