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
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                HorizontalTextAlign = UITextAlign.Start
            };
            
            var size = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(100, 0)
            };
            
            var percentage = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(100, 0)
            };

            assetType.Bind(Label.TextProperty, "AssetType");
            
            size.Bind(Label.TextProperty, "Size");
            size.Bind(BackgroundColorProperty, "SizeBackgroundColor");
            
            percentage.Bind(Label.TextProperty, "Percentage");
            percentage.Bind(BackgroundColorProperty, "PercentageBackgroundColor");
            
            Content = new LayoutView
            {
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