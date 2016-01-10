using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class Window : ViewBase
	{
		private UIRect absolutePosition;
		
		public override void Layout(UIRect rect)
		{
			absolutePosition = rect;

			foreach(var child in Children)
				child.Layout(parentRect : absolutePosition);
		}

		public override void Render()
		{
			NativeRenderer.Render(absolutePosition);

			foreach(var child in Children)
				child.Render();
		}
	}
}