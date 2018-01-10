using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Buttons;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Lists;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Views;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;
using Page = WellFired.Guacamole.Pages.Page;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages
{
	public class AssetsPage : Page
	{
		public AssetsPage()
		{
			Style = NoBackgroundNoOutline.Style;
			
			var pathHeader = new HeaderButtonView { Style = NoBackgroundNoOutline.Style, Text = "Path", HorizontalLayout = LayoutOptions.Fill, VerticalLayout = LayoutOptions.Fill };
			var importedSizeHeader = new HeaderButtonView { Style = NoBackgroundNoOutline.Style, Text = "Imp. Size", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000) };
			var rawSizeHeader = new HeaderButtonView { Style = NoBackgroundNoOutline.Style, Text = "Raw Size", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000) };
			var percentageHeader = new HeaderButtonView { Style = NoBackgroundNoOutline.Style, Text = "Per.", HorizontalLayout = LayoutOptions.Expand, MaxSize = UISize.Of(110, 1000) };
            
			pathHeader.Bind(ButtonView.ButtonPressedCommandProperty, "SortByAssetPath");
			importedSizeHeader.Bind(ButtonView.ButtonPressedCommandProperty, "SortByImportedSize");
			rawSizeHeader.Bind(ButtonView.ButtonPressedCommandProperty, "SortByRawSize");
			percentageHeader.Bind(ButtonView.ButtonPressedCommandProperty, "SortByPercentage");
            
			pathHeader.Bind(ButtonView.TextProperty, "AssetPathText");
			importedSizeHeader.Bind(ButtonView.TextProperty, "ImportedSizeText");
			rawSizeHeader.Bind(ButtonView.TextProperty, "RawSizeText");
			percentageHeader.Bind(ButtonView.TextProperty, "PercentageText");

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
						Children = { new LabelView { Style = NoBackgroundNoOutline.Style, Text = "Total Size" }, new TotalSizeLabelView() },
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