using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
    public class BuildReportDiffViewModel : ObservableBase
    {
        private readonly BuildReportDiff _buildReportDiff;

        private UIColor _buildSizeColor;
        public UIColor BuildSizeColor
        {
            set { SetProperty(ref _buildSizeColor, value); }
            get { return _buildSizeColor; }
        }

        public BuildReportDiffViewModel(BuildReportDiff buildReportDiff)
        {
            _buildReportDiff = buildReportDiff;
        }

        public void DetermineDiffView()
        {
            DetermineBuildSizeDiff();
        }

        private void DetermineBuildSizeDiff()
        {
            BuildSizeColor = _buildReportDiff.BuildSizeAreDiff ? UIColor.FromRGB(255, 0, 0) : UIColor.FromRGB(40, 40, 40);
        }
    }
}