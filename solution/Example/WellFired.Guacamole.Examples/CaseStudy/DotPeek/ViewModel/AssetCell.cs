using JetBrains.Annotations;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Utilities;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
    public class AssetCell : ObservableBase
    {
        private string _assetPath;
        private string _importedSize;
        private UIColor _importedSizeBackgroundColor;
        private string _rawSize;
        private UIColor _rawSizeBackgroundColor;
        private string _percentage;

        [PublicAPI]
        public string AssetPath
        {
            get => _assetPath;
            set => SetProperty(ref _assetPath, value);
        }

        [PublicAPI]
        public string ImportedSize
        {
            get => _importedSize;
            set => SetProperty(ref _importedSize, value);
        }

        [PublicAPI]
        public UIColor ImportedSizeBackgroundColor
        {
            get => _importedSizeBackgroundColor;
            set => SetProperty(ref _importedSizeBackgroundColor, value);
        }

        [PublicAPI]
        public string RawSize
        {
            get => _rawSize;
            set => SetProperty(ref _rawSize, value);
        }

        [PublicAPI]
        public UIColor RawSizeBackgroundColor
        {
            get => _rawSizeBackgroundColor;
            set => SetProperty(ref _rawSizeBackgroundColor, value);
        }
        
        [PublicAPI]
        public string Percentage
        {
            get => _percentage;
            set => SetProperty(ref _percentage, value);
        }
        
        public AssetCell(IAsset asset, IAsset previousAsset)
        {
            AssetPath = asset.Path;
            ImportedSize = $"{asset.ImportedSize.SizeInMb:0.00} MB";
            RawSize = $"{asset.RawSize.SizeInMb:0.00} MB";
            Percentage = asset.Percentage + "%";

            if (previousAsset != null)
            {
                GenerateDiff(asset, previousAsset);
            }
        }

        private void GenerateDiff(IAsset asset, IAsset previousAsset)
        {
            ImportedSizeBackgroundColor = ColorCompare.CompareSizeColor(asset.ImportedSize, previousAsset.ImportedSize);
            RawSizeBackgroundColor = ColorCompare.CompareSizeColor(asset.RawSize, previousAsset.RawSize);
        }
    }
}