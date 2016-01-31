using System.Collections.Generic;
using System.Linq;

namespace WellFired.Guacamole
{
	public class AdjacentLayout : ViewBase 
	{
		public OrientationOptions Orientation
		{
			get;
			set;
		}

		public int Spacing 
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

		public override void Layout()
		{
			base.Layout();

			var x = Padding.Left;
			var y = Padding.Top;
			switch(Orientation) 
			{
			case OrientationOptions.Horizontal:
				foreach(var child in Children) 
				{
					child.LayoutTo(x, y);
					x += (child.RectRequest.Width + Spacing);
				}
				break;
			case OrientationOptions.Vertical:
				foreach(var child in Children) 
				{
					child.LayoutTo(x, y);
					y += (child.RectRequest.Height + Spacing);
				}
				break;
			}
		}

		protected override UIRect CalculateValidRectRequest()
		{
			var totalWidth = 0;
			var totalHeight = 0;
			foreach(var child in Children)
			{
				var size = child.RectRequest;

				switch(Orientation) 
				{
				case OrientationOptions.Horizontal:
					totalWidth += (size.X + size.Width + Spacing);
					totalHeight = System.Math.Max(totalHeight, size.Height);
					break;
				case OrientationOptions.Vertical:
					totalHeight += (size.Y + size.Height + Spacing);
					totalWidth = System.Math.Max(totalWidth, size.Width);
					break;
				}
			}

			// This is done seperately as a final step, since we could potentially have hundreds of elements in our layout
			switch(Orientation) 
			{
			case OrientationOptions.Horizontal:
				totalWidth -= Spacing;
				break;
			case OrientationOptions.Vertical:
				totalHeight -= Spacing;
				break;
			}
				
			return new UIRect(0, 0, totalWidth, totalHeight);
		}
	}
}