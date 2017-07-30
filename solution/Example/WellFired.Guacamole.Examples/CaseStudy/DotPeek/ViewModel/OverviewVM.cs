using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
    public class OverviewVM : ObservableBase
    {
        private string _buildTime;

        private string _commitID;

        private string _platform;

        private string _unityVersion;
        private UIColor _unityVersionBackgroundColor;

        private string _buildSize;
        private UIColor _buildSizeBackgroundColor;

        [PublicAPI]
        public string BuildTime
        {
            get { return _buildTime; }
            set { SetProperty(ref _buildTime, value); }
        }

        [PublicAPI]
        public string CommitID
        {
            get { return _commitID; }
            set { SetProperty(ref _commitID, value); }
        }

        [PublicAPI]
        public string Platform
        {
            get { return _platform; }
            set { SetProperty(ref _platform, value); }
        }

        [PublicAPI]
        public string UnityVersion
        {
            get { return _unityVersion; }
            set { SetProperty(ref _unityVersion, value); }
        }

        [PublicAPI]
        public UIColor UnityVersionBackgroundColor
        {
            get { return _unityVersionBackgroundColor; }
            set { SetProperty(ref _unityVersionBackgroundColor, value); }
        }

        [PublicAPI]
        public string BuildSize
        {
            get { return _buildSize; }
            set { SetProperty(ref _buildSize, value); }
        }

        [PublicAPI]
        public UIColor BuildSizeBackgroundColor
        {
            get { return _buildSizeBackgroundColor; }
            set { SetProperty(ref _buildSizeBackgroundColor, value); }
        }

        public OverviewVM(BuildReport buildReport, BuildReport previousReport = null)
        {
            BuildTime = "Build Time : " + buildReport.BuildOverview.BuildTime.ToShortDateString();
            CommitID = "Commit ID : " + buildReport.BuildOverview.CommitID;
            Platform = "Platform : " + buildReport.BuildOverview.Platform;
            UnityVersion = buildReport.BuildOverview.UnityVersion;
            BuildSize = buildReport.BuildOverview.BuildSize.SizeInMb + " MB";
            if (previousReport != null)
            {
                GenerateDiff(buildReport, previousReport);
            }
        }

        private void GenerateDiff(BuildReport buildReport, BuildReport previousReport)
        {
            UnityVersionBackgroundColor =
                buildReport.BuildOverview.UnityVersion != previousReport.BuildOverview.UnityVersion
                    ? UIColor.FromRGB(0, 136, 43)
                    : UIColor.FromRGB(40, 40, 40);
            
            BuildSizeBackgroundColor =
                buildReport.BuildOverview.BuildSize != previousReport.BuildOverview.BuildSize
                    ? UIColor.FromRGB(0, 136, 43)
                    : UIColor.FromRGB(40, 40, 40);
        }
    }
}