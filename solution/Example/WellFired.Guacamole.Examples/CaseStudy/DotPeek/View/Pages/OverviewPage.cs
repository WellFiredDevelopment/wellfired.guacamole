using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Lists;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Views;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;
using Page = WellFired.Guacamole.Views.Page;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages
{
	public class OverviewPage : Page
	{
		public OverviewPage()
		{
			Style = Styles.Page.Style;
			
			Content = new DotPeekLayoutView {
				HorizontalLayout = LayoutOptions.Fill,
				Children = {
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "Build Time" }, new BuildTimeLabel() },
						Layout = new AdjacentLayout()
					},  
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "Commit Id" }, new CommitIdLabel() },
						Layout = new AdjacentLayout()
					},  
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "Platform" }, new PlatformLabel() },
						Layout = new AdjacentLayout(),
					}, 
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "Unity Version" }, new UnityVersionLabel() },
						Layout = new AdjacentLayout(),
					}, 
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "Build Size" }, new BuildSizeLabel() },
						Layout = new AdjacentLayout()
					}, 
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "Resources Asset Size" }, new ResourceAssetSizeLabel() },
						Layout = new AdjacentLayout()
					},
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "Asset Breakdown" }, new AssetBreakdownList() },
						Layout = new AdjacentLayout { Orientation = OrientationOptions.Vertical }
					}
				},
				Layout = new AdjacentLayout { Orientation = OrientationOptions.Vertical }
			};
		}
	}
}