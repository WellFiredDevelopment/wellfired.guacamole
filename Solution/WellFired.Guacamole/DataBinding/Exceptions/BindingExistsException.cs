using System;

namespace WellFired.Guacamole.DataBinding.Exceptions
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

	    public BindingExistsException(string forBinding) 
		{
			ForBinding = forBinding;
		}
	}
}