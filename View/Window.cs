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
			Content.AttemptToFullfillRequests(rect - Padding);

			FinalRenderedRect = rect;
			Content.Layout();
		}

		public override void Render(UIRect parentRect)
		{
			NativeRenderer.Render(renderRect : FinalRenderedRect);

			var relativeParentRect = new UIRect(0, 0, parentRect.Width, parentRect.Height);
			Content.Render(parentRect: relativeParentRect);
		}
	}
}