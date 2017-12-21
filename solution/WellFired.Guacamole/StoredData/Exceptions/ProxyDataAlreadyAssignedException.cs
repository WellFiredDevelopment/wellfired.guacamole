using System;

namespace WellFired.Guacamole.StoredData.Exceptions
{
	public class ProxyDataAlreadyAssignedException : Exception
	{
		private readonly string _key;

		public ProxyDataAlreadyAssignedException(string key)
		{
			_key = key;
		}

		public override string Message => $"A data proxy is already in used for the key {_key}.";
	}
}