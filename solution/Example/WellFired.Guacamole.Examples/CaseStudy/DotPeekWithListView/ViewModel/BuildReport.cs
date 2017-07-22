using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views.BindingContexts;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeekWithListView.ViewModel
{
    public class BuildReport : CellBindingContextBase
    {
        private int _buildTime;
        private string _platform;
        private string _unityVersion;
        private int _buildSize;
        private UIColor _buildSizeBackgroundColor;

        [PublicAPI]
        public int BuildTime
        {
            get { return _buildTime; }
            set { SetProperty(ref _buildTime, value); }
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
        public int BuildSize
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

        public BuildReport(Model.BuildReport buildReport, Model.BuildReport previousReport)
        {
            BuildTime = buildReport.BuildTime;
            Platform = buildReport.Platform;
            UnityVersion = buildReport.UnityVersion;
            BuildSize = buildReport.BuildSize;
            BuildSizeBackgroundColor = previousReport == default(Model.BuildReport) ? UIColor.GreenYellow : buildReport.BuildSize < previousReport.BuildSize ? UIColor.GreenYellow : UIColor.Red;
        }
    }
}