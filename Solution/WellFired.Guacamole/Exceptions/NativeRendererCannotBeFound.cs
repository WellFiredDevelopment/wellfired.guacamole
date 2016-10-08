﻿using System;

namespace WellFired.Guacamole.Exceptions
{
	public class NativeRendererCannotBeFound : Exception 
	{
		private string ForControl { get; }

		public override string Message => $"NativeRenderer Cannot be found for : {ForControl}";

	    public NativeRendererCannotBeFound(string forControl) 
		{
			ForControl = forControl;
		}
	}
}