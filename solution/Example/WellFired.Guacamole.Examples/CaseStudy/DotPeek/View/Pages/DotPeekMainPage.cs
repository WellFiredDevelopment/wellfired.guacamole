using System;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel;
using WellFired.Guacamole.Pages;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages
{
	public class DotPeekMainPage : TabbedPage
	{
		public DotPeekMainPage()
		{
			Style = Styles.Page.Style;
			
			ItemTemplate = DataTemplate.Of(o => {
				switch (o) {
					case Overview _:
						return new OverviewPage { Title = "Overview" };
					case UsedAssets _:
						return new AssetsPage { Title = "Used Assets" };
					case UnusedAssets _:
						return new AssetsPage { Title = "Unused Assets" };
					case ProjectSettings _:
						return new ProjectSettingsPage { Title = "Project Settings" };
				}

				throw new NotImplementedException();
			});
			
			Bind(ItemSourceProperty, "TabSource");
			Bind(SelectedPageProperty, "SelectedPage");
		}
	}
}