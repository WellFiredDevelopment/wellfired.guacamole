using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Cells;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using ListView = WellFired.Guacamole.Views.ListView;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Lists
{
	public class PreProcessorList : ListView
	{
		public PreProcessorList()
		{
			Style = NoBackgroundNoOutline.Style;
			EntrySize = 14;
			Spacing = 7;
			Orientation = OrientationOptions.Vertical;
			HorizontalLayout = LayoutOptions.Fill;
			VerticalLayout = LayoutOptions.Fill;
			ItemTemplate = DataTemplate.Of(typeof(PreProcessorCell));
            
			Bind(ItemSourceProperty, "PreProcessorsList", BindingMode.ReadOnly);
		}
	}
}