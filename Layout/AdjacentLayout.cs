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
			OutlineColor = UIColor.Clear;
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

		public override void AttemptToFullfillRequests(UIRect availableSpace)
		{
			ReAdjustTo(
				HorizontalLayout == LayoutOptions.Fill ? availableSpace.Width : RectRequest.Width, 
				VerticalLayout == LayoutOptions.Fill ? availableSpace.Height : RectRequest.Height
			);

			switch(Orientation) 
			{
			case OrientationOptions.Horizontal:
				var horizontalDynamicChildren = Children.Where (child => child.HorizontalLayout == LayoutOptions.Fill);
				var horizontalStaticChildren = Children.Except (horizontalDynamicChildren);
				var staticWidth = horizontalStaticChildren.Sum (child => child.RectRequest.Width);
				var sharedWidth = horizontalDynamicChildren.Count() == 0 ? 0 : (availableSpace.Width - staticWidth - Padding.Width - (Spacing * (Children.Count - 1))) / horizontalDynamicChildren.Count();

				// This is just to stop the UI from looking weird as hell if the user shrinks the UI too much.
				if(sharedWidth < 0)
					sharedWidth = 0;
				
				var newHeight = RectRequest.Height;
				if (VerticalLayout == LayoutOptions.Fill)
					newHeight = availableSpace.Height - Padding.Height;

				foreach(var child in horizontalDynamicChildren) 
				{
					var sharedAvailableSpace = new UIRect(availableSpace.X, availableSpace.Y, sharedWidth, newHeight);
					child.AttemptToFullfillRequests (availableSpace: sharedAvailableSpace);
				}
				foreach(var child in horizontalStaticChildren) 
				{
					var staticAvailableSpace = new UIRect(availableSpace.X, availableSpace.Y, child.RectRequest.Width, newHeight);
					child.AttemptToFullfillRequests (availableSpace: staticAvailableSpace);
				}
				break;
			case OrientationOptions.Vertical:
				var verticalDynamicChildren = Children.Where (child => child.VerticalLayout == LayoutOptions.Fill);
				var verticalStaticChildren = Children.Except (verticalDynamicChildren);
				var staticHeight = verticalStaticChildren.Sum (child => child.RectRequest.Height);
				var sharedHeight = verticalDynamicChildren.Count () == 0 ? 0 : (availableSpace.Height - Padding.Height - (Spacing * (Children.Count - 1)) - staticHeight) / verticalDynamicChildren.Count ();

				// This is just to stop the UI from looking weird as hell if the user shrinks the UI too much.
				if(sharedHeight < 0)
					sharedHeight = 0;

				var newWidth = RectRequest.Width;
				if (HorizontalLayout == LayoutOptions.Fill)
					newWidth = availableSpace.Width - Padding.Width;

				foreach(var child in verticalDynamicChildren) 
				{
					var sharedAvailableSpace = new UIRect(availableSpace.X, availableSpace.Y, newWidth, sharedHeight);
					child.AttemptToFullfillRequests (availableSpace: sharedAvailableSpace);
				}
				foreach(var child in verticalStaticChildren) 
				{
					var staticAvailableSpace = new UIRect(availableSpace.X, availableSpace.Y, newWidth, child.RectRequest.Height);
					child.AttemptToFullfillRequests (availableSpace: staticAvailableSpace);
				}
				break;
			}
		}
	}
}