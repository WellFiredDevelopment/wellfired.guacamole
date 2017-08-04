using System;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
    public class AssetSplitVM : ObservableBase
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
            get { return _assetType; }
            set { SetProperty(ref _assetType, value); }
        }

        [PublicAPI]
        public string Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }

        [PublicAPI]
        public UIColor SizeBackgroundColor
        {
            get { return _sizeBackgroundColor; }
            set { SetProperty(ref _sizeBackgroundColor, value); }
        }

        [PublicAPI]
        public string Percentage
        {
            get { return _percentage; }
            set { SetProperty(ref _percentage, value); }
        }

        [PublicAPI]
        public UIColor PercentageBackgroundColor
        {
            get { return _percentageBackgroundColor; }
            set { SetProperty(ref _percentageBackgroundColor, value); }
        }

        public AssetSplitVM(BuildOverview.BuildAssetSplit assetSplit, BuildOverview.BuildAssetSplit previousAssetSplit = null)
        {
            AssetType = assetSplit.Category.ToString();
            Size = $"{assetSplit.Size.SizeInMb:0.00} MB";
            Percentage = assetSplit.Percentage + "%";
            
            if (previousAssetSplit != null)
            {
                GenerateDiff(assetSplit, previousAssetSplit);
            }
        }

        private void GenerateDiff(BuildOverview.BuildAssetSplit assetSplit, BuildOverview.BuildAssetSplit previousAssetSplit)
        {
            SizeBackgroundColor = ViewModelUtils.CompareSizeColor(assetSplit.Size, previousAssetSplit.Size);
            
            PercentageBackgroundColor =
                Math.Abs(assetSplit.Percentage - previousAssetSplit.Percentage) >= Tolerance
                    ? UIColor.FromRGB(0, 136, 43)
                    : UIColor.FromRGB(40, 40, 40);
        }
    }
}