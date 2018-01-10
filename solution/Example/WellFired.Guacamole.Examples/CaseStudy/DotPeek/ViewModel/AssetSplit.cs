using System;
using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Utilities;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
    public class AssetSplit : ObservableBase
    {
        private const double Tolerance = 0.1;

        private string _assetType;
        private string _size;
        private UIColor _sizeBackgroundColor;
        private string _percentage;
        private UIColor _percentageBackgroundColor;

        [PublicAPI]
        public string AssetType
        {
            get => _assetType;
            set => SetProperty(ref _assetType, value);
        }

        [PublicAPI]
        public string Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        [PublicAPI]
        public UIColor SizeBackgroundColor
        {
            get => _sizeBackgroundColor;
            set => SetProperty(ref _sizeBackgroundColor, value);
        }

        [PublicAPI]
        public string Percentage
        {
            get => _percentage;
            set => SetProperty(ref _percentage, value);
        }

        [PublicAPI]
        public UIColor PercentageBackgroundColor
        {
            get => _percentageBackgroundColor;
            set => SetProperty(ref _percentageBackgroundColor, value);
        }

        public AssetSplit(BuildOverview.BuildAssetSplit assetSplit, BuildOverview.BuildAssetSplit previousAssetSplit)
        {
            Build(assetSplit);
            GenerateDiff(assetSplit, previousAssetSplit);
        }

        public AssetSplit(BuildOverview.BuildAssetSplit assetSplit)
        {
            Build(assetSplit);
        }

        private void Build(BuildOverview.BuildAssetSplit assetSplit)
        {
            AssetType = assetSplit.Category.ToString();
            Size = $"{assetSplit.Size.SizeInMb:0.00} MB";
            Percentage = assetSplit.Percentage + "%";
        }

        private void GenerateDiff(BuildOverview.BuildAssetSplit assetSplit, BuildOverview.BuildAssetSplit previousAssetSplit)
        {
            SizeBackgroundColor = ColorCompare.CompareSizeColor(assetSplit.Size, previousAssetSplit.Size);
            PercentageBackgroundColor = Math.Abs(assetSplit.Percentage - previousAssetSplit.Percentage) >= Tolerance
                    ? UIColor.FromRGB(0, 136, 43)
                    : UIColor.FromRGB(40, 40, 40);
        }
    }
}