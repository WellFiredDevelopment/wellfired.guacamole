using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;
using LayoutView = WellFired.Guacamole.Views.LayoutView;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Cells
{
	public class AssetBreakdownCell : Cell
	{
		public AssetBreakdownCell()
		{
			Style = NoBackgroundNoOutline.Style;
			HorizontalLayout = LayoutOptions.Fill;
			VerticalLayout = LayoutOptions.Expand;

			var assetType = new Label
			{
				Style = NoBackgroundNoOutline.Style,
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Fill,
				HorizontalTextAlign = UITextAlign.Start
			};
            
			var size = new Label
			{
				Style = NoBackgroundNoOutline.Style,
				HorizontalLayout = LayoutOptions.Expand,
				VerticalLayout = LayoutOptions.Fill,
				MinSize = UISize.Of(100, 0)
			};
            
			var percentage = new Label
			{
				Style = NoBackgroundNoOutline.Style,
				HorizontalLayout = LayoutOptions.Expand,
				VerticalLayout = LayoutOptions.Fill,
				MinSize = UISize.Of(100, 0)
			};

			assetType.Bind(Label.TextProperty, "AssetType");
			size.Bind(Label.TextProperty, "Size");
			percentage.Bind(Label.TextProperty, "Percentage");
            
			Content = new LayoutView
			{
				Style = NoBackgroundNoOutline.Style,
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Expand,
				Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
				MinSize = UISize.Of(0, 24),
				Children = {
					assetType, 
					size,
					percentage
				}
			};
		}
	}
}