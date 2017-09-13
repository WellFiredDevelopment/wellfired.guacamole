using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
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
                HorizontalTextAlign = UITextAlign.Start
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

            assetPath.Bind(Label.TextProperty, "AssetPath", BindingMode.ReadOnly);
            
            importedSize.Bind(Label.TextProperty, "ImportedSize", BindingMode.ReadOnly);
            importedSize.Bind(BackgroundColorProperty, "ImportedSizeBackgroundColor", BindingMode.ReadOnly);
            
            rawSize.Bind(Label.TextProperty, "RawSize", BindingMode.ReadOnly);
            rawSize.Bind(BackgroundColorProperty, "RawSizeBackgroundColor", BindingMode.ReadOnly);
            
            percentage.Bind(Label.TextProperty, "Percentage", BindingMode.ReadOnly);
            
            Content = new LayoutView
            {
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Expand,
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