using System;

namespace WellFired.Guacamole
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

		private NativeRendererCannotBeFound() {}

		public NativeRendererCannotBeFound(string forControl) 
		{
			ForControl = forControl;
		}
	}
}