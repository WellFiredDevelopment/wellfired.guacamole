using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class Window : ViewBase
	{
		public ViewBase Content { get; set; }
		
		public override void Layout(UIRect rect)
		{
			Content.InvalidateRectRequest();

			FinalRenderedRect = rect;
			Content.Layout(parentRect : FinalRenderedRect);
		}

		public override void Render()
		{
			NativeRenderer.Render(renderRect : FinalRenderedRect);
			Content.Render();
		}
	}
}