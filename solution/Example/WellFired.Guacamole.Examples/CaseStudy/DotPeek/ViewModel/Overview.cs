using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Utilities;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
	public class Overview : ObservableBase
	{
        private string _buildTime;
        private string _commitId;
        private string _platform;
        private string _unityVersion;
        private UIColor _unityVersionBackgroundColor;
        private string _buildSize;
        private UIColor _buildSizeBackgroundColor;
        private List<AssetSplit> _assetBreakdown;
        private string _resourceAssetsSize;
        private UIColor _resourceAssetsSizeBackgroundColor;

        [PublicAPI]
        public string BuildTime
        {
            get => _buildTime;
            set => SetProperty(ref _buildTime, value);
        }

        [PublicAPI]
        public string CommitId
        {
            get => _commitId;
            set => SetProperty(ref _commitId, value);
        }

        [PublicAPI]
        public string Platform
        {
            get => _platform;
            set => SetProperty(ref _platform, value);
        }

        [PublicAPI]
        public string UnityVersion
        {
            get => _unityVersion;
            set => SetProperty(ref _unityVersion, value);
        }

        [PublicAPI]
        public UIColor UnityVersionBackgroundColor
        {
            get => _unityVersionBackgroundColor;
            set => SetProperty(ref _unityVersionBackgroundColor, value);
        }

        [PublicAPI]
        public string BuildSize
        {
            get => _buildSize;
            set => SetProperty(ref _buildSize, value);
        }

        [PublicAPI]
        public UIColor BuildSizeBackgroundColor
        {
            get => _buildSizeBackgroundColor;
            set => SetProperty(ref _buildSizeBackgroundColor, value);
        }

        [PublicAPI]
        public List<AssetSplit> AssetBreakdown
        {
            get => _assetBreakdown;
            set => SetProperty(ref _assetBreakdown, value);
        }
        
        [PublicAPI]
        public string ResourceAssetsSize
        {
            get => _resourceAssetsSize;
            set => SetProperty(ref _resourceAssetsSize, value);
        }
        
        [PublicAPI]
        public UIColor ResourceAssetsSizeBackgroundColor
        {
            get => _resourceAssetsSizeBackgroundColor;
            set => SetProperty(ref _resourceAssetsSizeBackgroundColor, value);
        }

        public Overview(BuildReport buildReport, BuildReport previousReport)
        {
            BuildTime = $"{buildReport.BuildOverview.BuildTime.ToShortDateString()}";
            CommitId = $"{buildReport.BuildOverview.CommitId}";
            Platform = $"{buildReport.BuildOverview.Platform}";
            UnityVersion = $"{buildReport.BuildOverview.UnityVersion}";
            BuildSize = $"{buildReport.BuildOverview.BuildSize.SizeInMb:0.00} MB";

            GenerateAssetBreakdown(buildReport, previousReport);
            GenerateResourceAssetsSize(buildReport, previousReport);

            if (previousReport != BuildReport.Null)
                GenerateDiff(buildReport, previousReport);
        }

        private void GenerateResourceAssetsSize(BuildReport buildReport, BuildReport previousReport = null)
        {
            var size = new FileSize(0f);
            size = buildReport.ResourcesIncludedAssets.Aggregate(size, (current, asset) => current + asset.ImportedSize);
            ResourceAssetsSize = $"{size.SizeInMb:0.00} MB";

            if (previousReport == null)
                return;
            
            var previousSize = new FileSize(0f);
            previousSize = previousReport.ResourcesIncludedAssets.Aggregate(previousSize, (current, asset) => current + asset.ImportedSize);
                
            ResourceAssetsSizeBackgroundColor = ColorCompare.CompareSizeColor(size, previousSize);
        }

	    private void GenerateAssetBreakdown(BuildReport buildReport, BuildReport previousBuildReport)
	    {
	        if (previousBuildReport == BuildReport.Null)
	        {
	            AssetBreakdown = buildReport.BuildOverview.BuildAssetSplits.Select(o => new AssetSplit(o))
	                                        .ToList();
	            return;
	        }

	        AssetBreakdown = buildReport.BuildOverview.BuildAssetSplits
	                                    .Select(assetSplit => new AssetSplit(assetSplit, previousBuildReport.BuildOverview.BuildAssetSplits.Find(x => x.Category == assetSplit.Category)))
	                                    .ToList();
	    }

        private void GenerateDiff(BuildReport buildReport, BuildReport previousReport)
        {
            UnityVersionBackgroundColor = buildReport.BuildOverview.UnityVersion != previousReport.BuildOverview.UnityVersion
                    ? UIColor.FromRGB(0, 136, 43)
                    : UIColor.FromRGB(40, 40, 40);

            BuildSizeBackgroundColor = ColorCompare.CompareSizeColor(buildReport.BuildOverview.BuildSize, previousReport.BuildOverview.BuildSize);
        }
	}
}