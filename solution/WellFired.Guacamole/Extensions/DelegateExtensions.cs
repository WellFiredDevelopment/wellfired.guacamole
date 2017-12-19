using System;
using System.Linq;
using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.Extensions
{
	public static class DelegateExtensions
	{
		[PublicAPI]
		public static bool AlreadyHasSubscriber(this Delegate container, Delegate entry)
		{
			var list = container.GetInvocationList();
			return list.Any(t => t == entry);
		}
	}
}