using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class Window : ViewBase
	{
		private UIRect FinalRenderedRect { get; set; }
		public ViewBase Content { get; set; }
		
		public void Layout(UIRect rect)
		{
			Content.CalculateRectRequest();

			FinalRenderedRect = rect;
			Content.Layout();
		}

		public override void Render()
		{
			NativeRenderer.Render(renderRect : FinalRenderedRect);
			Content.Render();
		}
	}
}