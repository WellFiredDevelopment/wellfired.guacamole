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
			relativeParentRect -= Padding;
			Content.Render(parentRect: relativeParentRect);
		}

		public override void PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.PropertyChanged(sender, e);

			if(e.PropertyName == BindingContextProperty.PropertyName)
				Content.BindingContext = BindingContext;
		}
	}
}