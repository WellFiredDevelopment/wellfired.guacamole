using System;

namespace WellFired.Guacamole
{
	public interface INativeRenderer
	{
		ViewBase Control
		{
			get;
			set;
		}

		void Render(UIRect renderRect);
	}
}