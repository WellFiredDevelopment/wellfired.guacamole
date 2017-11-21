using System;

namespace WellFired.Guacamole.Exceptions
{
	public class ViewRenderingException : Exception
	{
		private readonly Type _viewType;
		private readonly string _id;
		private readonly string _message;
		private readonly string _stacktrace;

		public ViewRenderingException(Type viewType, string id, string message, string stacktrace)
		{
			_viewType = viewType;
			_id = id;
			_message = message;
			_stacktrace = stacktrace;
		}

		public override string Message => $"An error happened while rendering view of type : {_viewType} and ID : {_id} :\n{_message}\n{_stacktrace}";
	}
}