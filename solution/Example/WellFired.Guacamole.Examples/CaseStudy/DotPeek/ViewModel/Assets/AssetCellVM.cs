using JetBrains.Annotations;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Assets
{
    public class AssetCellVM : ObservableBase
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
            get { return _assetPath; }
            set { SetProperty(ref _assetPath, value); }
        }

        [PublicAPI]
        public string ImportedSize
        {
            get { return _importedSize; }
            set { SetProperty(ref _importedSize, value); }
        }

        [PublicAPI]
        public UIColor ImportedSizeBackgroundColor
        {
            get { return _importedSizeBackgroundColor; }
            set { SetProperty(ref _importedSizeBackgroundColor, value); }
        }

        [PublicAPI]
        public string RawSize
        {
            get { return _rawSize; }
            set { SetProperty(ref _rawSize, value); }
        }

        [PublicAPI]
        public UIColor RawSizeBackgroundColor
        {
            get { return _rawSizeBackgroundColor; }
            set { SetProperty(ref _rawSizeBackgroundColor, value); }
        }
        
        [PublicAPI]
        public string Percentage
        {
            get { return _percentage; }
            set { SetProperty(ref _percentage, value); }
        }
        
        public AssetCellVM(IAsset asset, IAsset previousAsset)
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
            ImportedSizeBackgroundColor = ViewModelUtils.CompareSizeColor(asset.ImportedSize, previousAsset.ImportedSize);
            RawSizeBackgroundColor = ViewModelUtils.CompareSizeColor(asset.RawSize, previousAsset.RawSize);
        }
    }
}