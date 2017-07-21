namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
    public class BuildReportDiff
    {
        public bool BuildSizeAreDiff { get; private set; }
        
        public BuildReportDiff(BuildReport leftReport, BuildReport rightReport)
        {
            BuildSizeAreDiff = leftReport.BuildOverview.BuildSize != rightReport.BuildOverview.BuildSize;
        }
    }
}