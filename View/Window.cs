using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class Window : ViewBase
	{
		private UIRect absolutePosition;
		
		public override void Layout(UIRect rect)
		{
			absolutePosition = rect;
		}

		public override void Render()
		{
			NativeRenderer.Render(absolutePosition);
		}
	}
}