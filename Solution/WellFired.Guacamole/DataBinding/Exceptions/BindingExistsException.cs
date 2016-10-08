using System;

namespace WellFired.Guacamole.DataBinding.Exceptions
{
	public class BindingExistsException : Exception 
	{
		private string ForBinding { get; }

		public override string Message => $"Binding already exists for : {ForBinding}, please check for duplicates";

	    public BindingExistsException(string forBinding) 
		{
			ForBinding = forBinding;
		}
	}
}