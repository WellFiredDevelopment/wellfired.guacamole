using System;

namespace WellFired.Guacamole.DataStorage.Exceptions
{
	public class AlreadyInitializeException : Exception
	{
		public override string Message => "An instance of thread synchronizer already exists.";
	}
}