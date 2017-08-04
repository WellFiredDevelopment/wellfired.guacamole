using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.OverviewPage
{
    public class AssetSplitCell : Cell
    {
        public AssetSplitCell()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Expand;
            BackgroundColor = UIColor.Black;

            var assetType = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
            };
            
            var size = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                MinSize = UISize.Of(80, 0)
            };
            
            var percentage = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                MinSize = UISize.Of(80, 0)
            };

            assetType.Bind(Label.TextProperty, "AssetType");
            
            size.Bind(Label.TextProperty, "Size");
            size.Bind(BackgroundColorProperty, "SizeBackgroundColor");
            
            percentage.Bind(Label.TextProperty, "Percentage");
            percentage.Bind(BackgroundColorProperty, "PercentageBackgroundColor");
            
            Content = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                Children = {
                    assetType, 
                    size,
                    percentage
                }
            };
        }
    }
}