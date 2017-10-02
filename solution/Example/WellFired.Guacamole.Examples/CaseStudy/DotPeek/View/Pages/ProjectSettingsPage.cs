using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Lists;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Views;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;
using Page = WellFired.Guacamole.Views.Page;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages
{
	public class ProjectSettingsPage : Page
	{
		public ProjectSettingsPage()
		{   
			Style = Styles.Page.Style;
			
			Content = new DotPeekLayoutView
			{
				HorizontalLayout = LayoutOptions.Fill,
				Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "PreProcessors :" }, new PreProcessorList() },
				Layout = new AdjacentLayout()
			};
		}
	}
}