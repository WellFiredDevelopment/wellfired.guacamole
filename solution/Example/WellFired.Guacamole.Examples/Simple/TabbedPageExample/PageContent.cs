using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.TabbedPageExample
{
	public static class PageContent
	{
		public static IView Create()
		{
			return LayoutView.With(new ILayoutable[] {
				new LabelView {Text = "Label"},
				new LabelView {Text = "Label"}
			}, AdjacentLayout.Of(OrientationOptions.Vertical));
		}
	}
}