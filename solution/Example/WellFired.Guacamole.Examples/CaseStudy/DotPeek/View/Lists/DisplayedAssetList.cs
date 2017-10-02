using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Cells;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using ListView = WellFired.Guacamole.Views.ListView;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Lists
{
	public class DisplayedAssetList : ListView
	{
		public DisplayedAssetList()
		{
			Style = NoBackgroundNoOutline.Style;
			EntrySize = 14;
			Spacing = 0;
			Orientation = OrientationOptions.Vertical;
			VerticalLayout = LayoutOptions.Expand;
			ItemTemplate = DataTemplate.Of(typeof(AssetCell));
            
			Bind(ItemSourceProperty, "DisplayedAssetsList", BindingMode.ReadOnly);
		}
	}
}