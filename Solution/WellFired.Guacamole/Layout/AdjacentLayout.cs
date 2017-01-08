using System;
using System.Linq;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Layout
{
	public class AdjacentLayout : ViewBase
	{
		public AdjacentLayout()
		{
			OutlineColor = UIColor.Clear;
			Orientation = OrientationOptions.Vertical;
			HorizontalLayout = LayoutOptions.Expand;
			VerticalLayout = LayoutOptions.Expand;
		}

		public OrientationOptions Orientation { private get; set; }
		public int Spacing { private get; set; }

		public override void Layout()
		{
			base.Layout();

			var x = Padding.Left;
			var y = Padding.Top;
			switch (Orientation)
			{
				case OrientationOptions.Horizontal:
					foreach (var child in Children)
					{
						child.LayoutTo(x, y);
						x += child.RectRequest.Width + Spacing;
					}
					break;
				case OrientationOptions.Vertical:
					foreach (var child in Children)
					{
						child.LayoutTo(x, y);
						y += child.RectRequest.Height + Spacing;
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected override UIRect CalculateValidRectRequest()
		{
			var totalWidth = 0;
			var totalHeight = 0;
			foreach (var child in Children)
			{
				var size = child.RectRequest;

				switch (Orientation)
				{
					case OrientationOptions.Horizontal:
						totalWidth += size.X + size.Width + Spacing;
						totalHeight = Math.Max(totalHeight, size.Height);
						break;
					case OrientationOptions.Vertical:
						totalHeight += size.Y + size.Height + Spacing;
						totalWidth = Math.Max(totalWidth, size.Width);
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
			}

			// This is done seperately as a final step, since we could potentially have hundreds of elements in our layout
			switch (Orientation)
			{
				case OrientationOptions.Horizontal:
					totalWidth -= Spacing;
					break;
				case OrientationOptions.Vertical:
					totalHeight -= Spacing;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return new UIRect(0, 0, Math.Max(totalWidth, MinSize.Width), Math.Max(totalHeight, MinSize.Height));
		}

		public override void AttemptToFullfillRequests(UIRect availableSpace)
		{
			ReAdjustTo(
				HorizontalLayout == LayoutOptions.Fill ? availableSpace.Width : RectRequest.Width,
				VerticalLayout == LayoutOptions.Fill ? availableSpace.Height : RectRequest.Height);

			switch (Orientation)
			{
				case OrientationOptions.Horizontal:
					var horizontalDynamicChildren = Children.Where(
						child => child.HorizontalLayout == LayoutOptions.Fill);
					var dynamicChildren = horizontalDynamicChildren as ViewBase[] ?? horizontalDynamicChildren.ToArray();
					var horizontalStaticChildren = Children.Except(dynamicChildren);
					var staticChildren = horizontalStaticChildren as ViewBase[] ?? horizontalStaticChildren.ToArray();
					var staticWidth = staticChildren.Sum(child => child.RectRequest.Width);
					var sharedWidth = !dynamicChildren.Any()
						? 0
						: (availableSpace.Width - staticWidth - Padding.Width -
						   Spacing*(Children.Count - 1))/dynamicChildren.Length;

					// This is just to stop the UI from looking weird as hell if the user shrinks the UI too much.
					if (sharedWidth < 0)
						sharedWidth = 0;

					var newHeight = RectRequest.Height;
					if (VerticalLayout == LayoutOptions.Fill)
						newHeight = availableSpace.Height - Padding.Height;

					foreach (var child in dynamicChildren)
					{
						var sharedAvailableSpace = new UIRect(
							availableSpace.X,
							availableSpace.Y,
							sharedWidth,
							newHeight);
						child.AttemptToFullfillRequests(sharedAvailableSpace);
					}
					foreach (var child in staticChildren)
					{
						var staticAvailableSpace = new UIRect(
							availableSpace.X,
							availableSpace.Y,
							child.RectRequest.Width,
							newHeight);
						child.AttemptToFullfillRequests(staticAvailableSpace);
					}
					break;
				case OrientationOptions.Vertical:
					var verticalDynamicChildren = Children.Where(child => child.VerticalLayout == LayoutOptions.Fill);
					var viewBases = verticalDynamicChildren as ViewBase[] ?? verticalDynamicChildren.ToArray();
					var verticalStaticChildren = Children.Except(viewBases);
					var enumerable = verticalStaticChildren as ViewBase[] ?? verticalStaticChildren.ToArray();
					var staticHeight = enumerable.Sum(child => child.RectRequest.Height);
					var sharedHeight = !viewBases.Any()
						? 0
						: (availableSpace.Height - Padding.Height - Spacing*(Children.Count - 1) -
						   staticHeight)/viewBases.Length;

					// This is just to stop the UI from looking weird as hell if the user shrinks the UI too much.
					if (sharedHeight < 0)
						sharedHeight = 0;

					var newWidth = RectRequest.Width;
					if (HorizontalLayout == LayoutOptions.Fill)
						newWidth = availableSpace.Width - Padding.Width;

					foreach (var child in viewBases)
					{
						var sharedAvailableSpace = new UIRect(
							availableSpace.X,
							availableSpace.Y,
							newWidth,
							sharedHeight);
						child.AttemptToFullfillRequests(sharedAvailableSpace);
					}
					foreach (var child in enumerable)
					{
						var staticAvailableSpace = new UIRect(
							availableSpace.X,
							availableSpace.Y,
							newWidth,
							child.RectRequest.Height);
						child.AttemptToFullfillRequests(staticAvailableSpace);
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}