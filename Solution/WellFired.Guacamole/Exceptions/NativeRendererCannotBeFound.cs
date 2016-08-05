using System;

namespace WellFired.Guacamole.Exceptions
{
	public class NativeRendererCannotBeFound : Exception 
	{
		private string ForControl {
			set;
			get;
		}

		public override string Message 
		{
			get 
			{
				return string.Format("NativeRenderer Cannot be found for : {0}", ForControl);
			}
		}

	    public NativeRendererCannotBeFound(string forControl) 
		{
			ForControl = forControl;
		}
	}
}