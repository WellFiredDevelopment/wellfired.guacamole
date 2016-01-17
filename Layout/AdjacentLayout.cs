using System.Collections.Generic;

namespace WellFired.Guacamole
{
	public class AdjacentLayout : ViewBase 
	{
		public OrientationOptions Orientation
		{
			get;
			set;
		}

		public AdjacentLayout()
		{
			Orientation = OrientationOptions.Vertical;
			HorizontalLayout = LayoutOptions.Expand;
			VerticalLayout = LayoutOptions.Expand;
		}

		public override void Layout(UIRect parentRect)
		{
			var requestedRect = RectRequest;
			if(HorizontalLayout == LayoutOptions.Fill)
				requestedRect.Width = parentRect.Width;
			if(VerticalLayout == LayoutOptions.Fill)
				requestedRect.Height = parentRect.Height;

			requestedRect.X += Padding.Left;
			requestedRect.Y += Padding.Top;
			requestedRect.Width -= Padding.Width;
			requestedRect.Height -= Padding.Height;

			FinalRenderedRect = requestedRect;
		}

		public override void Render ()
		{
			NativeRenderer.Render(renderRect : FinalRenderedRect);
		}

		protected override UIRect CalculateRect()
		{
			base.CalculateRect();

			var totalWidth = 0;
			var totalHeight = 0;
			foreach(var child in Children)
			{
				var size = child.RectRequest;

				switch(Orientation) 
				{
				case OrientationOptions.Horizontal:
					totalWidth += (size.X + size.Width);
					totalHeight = System.Math.Max(totalHeight, size.Height);
					break;
				case OrientationOptions.Vertical:
					totalHeight += (size.Y + size.Height);
					totalWidth = System.Math.Max(totalWidth, size.Width);
					break;
				}
			}
				
			return new UIRect(0, 0, totalWidth, totalHeight);
		}
	}
}