using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Pages.OverviewPage
{
    public class OverviewPage : Page
    {
        public OverviewPage()
        {
            var buildTimeAndCommitID = OverviewPageUICreator.GenerateBuildTimeAndCommitID();
            var platform = OverviewPageUICreator.GeneratePlatform();

            var unityVersion = OverviewPageUICreator.GenerateUnityVersion();
            unityVersion.Padding = UIPadding.With(0, 20, 0, 0);

            var buildSize = OverviewPageUICreator.GenerateBuildSize();
            buildSize.Padding = UIPadding.With(0, 20, 0, 0);

            var buildAssetSplit = OverviewPageUICreator.GenerateBuildAssetSplit();

            var resourceAssetsSize = OverviewPageUICreator.GenerateResourceAssetsSize();
            resourceAssetsSize.Padding = UIPadding.With(0, 10, 0, 0);

            var verticalLayout = Layout.LayoutFactory.CreateVerticalLayout(buildTimeAndCommitID, platform, unityVersion,
                buildSize, buildAssetSplit, resourceAssetsSize);
            
            verticalLayout.Padding = UIPadding.With(20, 50, 20, 0);
            
            Content = verticalLayout;

            BackgroundColor = UIColor.FromRGB(40, 40, 40);
        }
    }
}