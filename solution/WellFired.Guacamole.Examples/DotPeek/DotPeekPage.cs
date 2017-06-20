using WellFired.Guacamole.Examples.DotPeek.Layout;
using WellFired.Guacamole.Examples.DotPeek.Model;
using WellFired.Guacamole.Examples.DotPeek.UIElementFactory;
using WellFired.Guacamole.Examples.DotPeek.ViewModel;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.DotPeek
{
    public class DotPeekPage : Page
    {
        public DotPeekPage()
        {
            var leftReport =
                new BuildReport
                {
                    BuildOverview = new BuildOverview()
                    {
                        BuildSize = new FileSize(2024)
                    }
                };
            
            var rightReport =
                new BuildReport
                {
                    BuildOverview = new BuildOverview()
                    {
                        BuildSize = new FileSize(1024)
                    }
                };

            var buildReportDiff = new BuildReportDiff(leftReport, rightReport);
            var buildReportDiffViewModel = new BuildReportDiffViewModel(buildReportDiff);
            

            var buildTime = DotPeekLabelFactory.Create("Build Time :", "10/06/2017 - 17:03");
            var gitCommitID = DotPeekLabelFactory.Create("Commit ID :", "67ea1f1");
            var platform = DotPeekLabelFactory.Create("Platform :", "MacOS");
            var unityVersion = DotPeekLabelFactory.Create("Unity Version :", "Unity 5.5.1f1");
            var buildSize = DotPeekLabelFactory.Create("Build size :", "199MB", "BuildSizeColor");
            
            buildSize.BindingContext = buildReportDiffViewModel;
            buildReportDiffViewModel.DetermineDiffView();

            Content =
                LayoutFactory.CreateVerticalLayout
                (
                    LayoutFactory.CreateHorizontalLayout(buildTime, gitCommitID),
                    platform,
                    unityVersion,
                    buildSize
                );
        }
    }
}