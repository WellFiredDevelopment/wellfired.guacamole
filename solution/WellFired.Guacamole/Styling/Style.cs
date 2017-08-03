using System.Collections.Generic;

namespace WellFired.Guacamole.Styling
{
	public class Style : IStyle
	{
		public IList<ISetter> Setters { get; } = new List<ISetter>();
		public IList<ITrigger> Triggers { get; } = new List<ITrigger>();
	}
}