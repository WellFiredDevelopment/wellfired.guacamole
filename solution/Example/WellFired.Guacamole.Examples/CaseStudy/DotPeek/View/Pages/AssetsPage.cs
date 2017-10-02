using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Buttons;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Lists;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Views;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;
using Page = WellFired.Guacamole.Views.Page;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages
{
	public class AssetsPage : Page
	{
		public AssetsPage()
		{
			Style = NoBackgroundNoOutline.Style;
			
			var pathHeader = new HeaderButton { Style = NoBackgroundNoOutline.Style, Text = "Path", HorizontalLayout = LayoutOptions.Fill, VerticalLayout = LayoutOptions.Fill };
			var importedSizeHeader = new HeaderButton { Style = NoBackgroundNoOutline.Style, Text = "Imp. Size", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000) };
			var rawSizeHeader = new HeaderButton { Style = NoBackgroundNoOutline.Style, Text = "Raw Size", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000) };
			var percentageHeader = new HeaderButton { Style = NoBackgroundNoOutline.Style, Text = "Per.", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000) };
            
			pathHeader.Bind(Button.ButtonPressedCommandProperty, "SortByAssetPath");
			importedSizeHeader.Bind(Button.ButtonPressedCommandProperty, "SortByImportedSize");
			rawSizeHeader.Bind(Button.ButtonPressedCommandProperty, "SortByRawSize");
			percentageHeader.Bind(Button.ButtonPressedCommandProperty, "SortByPercentage");
            
			pathHeader.Bind(Button.TextProperty, "AssetPathText");
			importedSizeHeader.Bind(Button.TextProperty, "ImportedSizeText");
			rawSizeHeader.Bind(Button.TextProperty, "RawSizeText");
			percentageHeader.Bind(Button.TextProperty, "PercentageText");

			var listHeader = new DotPeekLayoutView
			{
				HorizontalLayout = LayoutOptions.Fill,
				Children = {pathHeader, importedSizeHeader, rawSizeHeader, percentageHeader},
				Layout = new AdjacentLayout {Orientation = OrientationOptions.Horizontal}
			};

			HorizontalLayout = LayoutOptions.Fill;
			
			Content = new DotPeekLayoutView {
				HorizontalLayout = LayoutOptions.Fill,
				Children =
				{
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { new Label { Style = NoBackgroundNoOutline.Style, Text = "Total Size" }, new TotalSizeLabel() },
						Layout = new AdjacentLayout()
					}, 
					new DotPeekLayoutView {
						HorizontalLayout = LayoutOptions.Fill,
						Children = { listHeader, new DisplayedAssetList() },
						Layout = new AdjacentLayout { Orientation = OrientationOptions.Vertical }
					}
				},
				Layout = new AdjacentLayout { Orientation = OrientationOptions.Vertical }
			}; 
		}
	}
}