using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Overview
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

        private List<AssetSplitVM> _assetSplitList;
        
        private string _resourceAssetsSize;
        private UIColor _resourceAssetsSizeBackgroundColor;

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

        [PublicAPI]
        public List<AssetSplitVM> AssetSplitList
        {
            get { return _assetSplitList; }
            set { SetProperty(ref _assetSplitList, value); }
        }
        
        [PublicAPI]
        public string ResourceAssetsSize
        {
            get { return _resourceAssetsSize; }
            set { SetProperty(ref _resourceAssetsSize, value); }
        }
        
        [PublicAPI]
        public UIColor ResourceAssetsSizeBackgroundColor
        {
            get { return _resourceAssetsSizeBackgroundColor; }
            set { SetProperty(ref _resourceAssetsSizeBackgroundColor, value); }
        }

        public OverviewVM(BuildReport buildReport, BuildReport previousReport = null)
        {
            BuildTime = "Build Time : " + buildReport.BuildOverview.BuildTime.ToShortDateString();
            CommitID = "Commit ID : " + buildReport.BuildOverview.CommitID;
            Platform = "Platform : " + buildReport.BuildOverview.Platform;
            UnityVersion = buildReport.BuildOverview.UnityVersion;
            BuildSize = $"{buildReport.BuildOverview.BuildSize.SizeInMb:0.00} MB";

            GenerateAssetSplitList(buildReport.BuildOverview.BuildAssetSplits,
                previousReport?.BuildOverview.BuildAssetSplits);

            GenerateResourceAssetsSize(buildReport, previousReport);

            if (previousReport != null)
            {
                GenerateDiff(buildReport, previousReport);
            }
        }

        private void GenerateResourceAssetsSize(BuildReport buildReport, BuildReport previousReport = null)
        {
            var size = new FileSize(0f);
            size = buildReport.ResourcesIncludedAssets.Aggregate(size, (current, asset) => current + asset.ImportedSize);
            ResourceAssetsSize = $"{size.SizeInMb:0.00} MB";

            if (previousReport != null)
            {
                var previousSize = new FileSize(0f);
                previousSize = previousReport.ResourcesIncludedAssets.Aggregate(previousSize, (current, asset) => current + asset.ImportedSize);
                
                ResourceAssetsSizeBackgroundColor =
                    ViewModelUtils.CompareSizeColor(size, previousSize);
            }
        }

        private void GenerateAssetSplitList(IEnumerable<BuildOverview.BuildAssetSplit> assetSplits,
            List<BuildOverview.BuildAssetSplit> previousAssetSplits = null)
        {
            var assetSplitVMList = new List<AssetSplitVM>();
            
            foreach (var assetSplit in assetSplits)
            {
                AssetSplitVM assetSplitVM;
                if (previousAssetSplits != null)
                {
                    assetSplitVM = new AssetSplitVM(assetSplit,
                        previousAssetSplits.Find(x => x.Category == assetSplit.Category));
                }
                else
                {
                    assetSplitVM = new AssetSplitVM(assetSplit);
                }

                assetSplitVMList.Add(assetSplitVM);
            }

            AssetSplitList = assetSplitVMList;
        }

        private void GenerateDiff(BuildReport buildReport, BuildReport previousReport)
        {
            UnityVersionBackgroundColor =
                buildReport.BuildOverview.UnityVersion != previousReport.BuildOverview.UnityVersion
                    ? UIColor.FromRGB(0, 136, 43)
                    : UIColor.FromRGB(40, 40, 40);

            BuildSizeBackgroundColor =
                ViewModelUtils.CompareSizeColor(buildReport.BuildOverview.BuildSize, previousReport.BuildOverview.BuildSize);
        }

        
    }
}