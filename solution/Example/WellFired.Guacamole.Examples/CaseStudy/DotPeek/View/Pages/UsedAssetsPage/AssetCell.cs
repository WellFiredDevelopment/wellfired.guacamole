using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.UsedAssetsPage
{
    public class AssetCell : Cell
    {
        public AssetCell()
        {
            HorizontalLayout = LayoutOptions.Fill;
            VerticalLayout = LayoutOptions.Expand;
            BackgroundColor = UIColor.Black;

            var assetPath = new Label
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
            };
            
            var importedSize = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };
            
            var rawSize = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };
            
            var percentage = new Label
            {
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Fill,
                MinSize = UISize.Of(80, 0)
            };

            assetPath.Bind(Label.TextProperty, "AssetPath");
            
            importedSize.Bind(Label.TextProperty, "ImportedSize");
            importedSize.Bind(BackgroundColorProperty, "ImportedSizeBackgroundColor");
            
            rawSize.Bind(Label.TextProperty, "RawSize");
            rawSize.Bind(BackgroundColorProperty, "RawSizeBackgroundColor");
            
            percentage.Bind(Label.TextProperty, "Percentage");
            
            Content = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
                Children = {
                    assetPath, 
                    importedSize,
                    rawSize,
                    percentage
                }
            };
        }
    }
}