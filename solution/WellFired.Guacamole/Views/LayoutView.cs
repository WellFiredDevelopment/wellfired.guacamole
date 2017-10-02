using System.Collections.Generic;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
	public partial class LayoutView : ViewWithChildren, ICanLayout
	{
	    public LayoutView()
		{
		    OutlineColor = UIColor.Clear;
			HorizontalLayout = LayoutOptions.Fill;
			VerticalLayout = LayoutOptions.Expand;
		}

		public static LayoutView WithAdjacentHorizontal(IList<ILayoutable> children)
		{
			return With(children, AdjacentLayout.Of(OrientationOptions.Horizontal));
		}

		public static LayoutView WithAdjacentVertical(IList<ILayoutable> children)
		{
			return With(children, AdjacentLayout.Of(OrientationOptions.Vertical));
		}

		public static LayoutView With(IList<ILayoutable> children, ILayoutChildren layout)
		{
			return new LayoutView {
				Children = children,
				Layout = layout
			};
		}
	}
}