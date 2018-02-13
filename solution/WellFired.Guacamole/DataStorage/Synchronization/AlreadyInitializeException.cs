using System;

namespace WellFired.Guacamole.DataStorage.Synchronization
{
	public class AlreadyInitializeException : Exception
	{
		public override string Message => "An instance of thread synchronizer already exists.";
	}
}