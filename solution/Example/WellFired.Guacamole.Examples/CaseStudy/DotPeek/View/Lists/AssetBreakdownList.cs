using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Cells;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using ListView = WellFired.Guacamole.Views.ListView;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Lists
{
	public class AssetBreakdownList : ListView
	{
		public AssetBreakdownList()
		{
			Style = NoBackgroundNoOutline.Style;
			EntrySize = 24;
			Orientation = OrientationOptions.Vertical;
			HorizontalLayout = LayoutOptions.Fill;
			VerticalLayout = LayoutOptions.Expand;
			ItemTemplate = DataTemplate.Of(typeof(AssetBreakdownCell));
			Bind(ItemSourceProperty, "AssetBreakdown", BindingMode.ReadOnly);
		}
	}
}