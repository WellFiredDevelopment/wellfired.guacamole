using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.UsedAssetsPage
{
    public class AssetsPage : Page
    {
        public AssetsPage()
        {
            var totalSize = AssetsPageUICreator.GenerateTotalSize();
            var assetList = AssetsPageUICreator.GenerateUsedAssetsList();
            
            var verticalLayout = Layout.LayoutFactory.CreateVerticalLayout(totalSize, assetList);
            
            verticalLayout.Padding = UIPadding.With(20, 50, 20, 0);
            
            Content = verticalLayout;

            BackgroundColor = UIColor.FromRGB(40, 40, 40);
        }
    }
}