using System;

namespace WellFired.Guacamole
{
	public class BindingExistsException : Exception 
	{
		private string ForBinding {
			set;
			get;
		}

		public override string Message 
		{
			get 
			{
				return string.Format("Binding already exists for : {0}, please check for duplicates", ForBinding);
			}
		}

		private BindingExistsException() {}

		public BindingExistsException(string forBinding) 
		{
			ForBinding = forBinding;
		}
	}
}