using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
    public class AssetsVM : ObservableBase
    {
        private List<AssetCellVM> _assetsList;
        
        private string _totalSize;
        private UIColor _totalSizeBackgroundColor;
        private Command _sortByPath;
        private List<AssetCellVM> _displayedAssetsList;

        [PublicAPI]
        public List<AssetCellVM> DisplayedAssetsList
        {
            get { return _displayedAssetsList; }
            set { SetProperty(ref _displayedAssetsList, value); }
        }
        
        [PublicAPI]
        public string TotalSize
        {
            get { return _totalSize; }
            set { SetProperty(ref _totalSize, value); }
        }

        [PublicAPI]
        public UIColor TotalSizeBacgroundColor
        {
            get { return _totalSizeBackgroundColor; }
            set { SetProperty(ref _totalSizeBackgroundColor, value); }
        }
        
        [PublicAPI]
        public Command SortByPath
        {
            get { return _sortByPath; }
            set { SetProperty(ref _sortByPath, value); }
        }

        public AssetsVM(List<IAsset> assets, List<IAsset> previousAssets = null)
        {
            GenerateTotalSize(assets, previousAssets);
            //DisplayedAssetsList = new List<AssetCellVM>();
            GenerateAssetsList(assets, previousAssets);
            SortByPath = new Command { ExecuteAction = () => DoSortByPath() };
            //SortByPath.Execute();
        }

        private void GenerateTotalSize(List<IAsset> assets, List<IAsset> previousAssets = null)
        {
            var size = new FileSize(0f);
            size = assets.Aggregate(size, (current, asset) => current + asset.ImportedSize);
            TotalSize = $"{size.SizeInMb:0.00} MB";
            
            if (previousAssets != null)
            {
                var previousSize = new FileSize(0f);
                previousSize = previousAssets.Aggregate(previousSize, (current, asset) => current + asset.ImportedSize);
                
                TotalSizeBacgroundColor =
                    ViewModelUtils.CompareSizeColor(size, previousSize);
            }
        }

        private void GenerateAssetsList(List<IAsset> assets, List<IAsset> previouslyAssets = null)
        {
//            _assetsList = new List<AssetCellVM>();
//            
//                foreach (var asset in assets)
//                {
//                    var previousIdenticalAsset = previouslyAssets?.Find(
//                        x => string.Equals(x.Path, asset.Path, StringComparison.Ordinal)
//                    );
//                
//                    _assetsList.Add(new AssetCellVM(asset, previousIdenticalAsset));
//                }
//
//            DisplayedAssetsList = new List<AssetCellVM>(_assetsList);
            TaskEx.Run(() =>
            {
                _assetsList = new List<AssetCellVM>();
            
                foreach (var asset in assets)
                {
                    var previousIdenticalAsset = previouslyAssets?.Find(
                        x => string.Equals(x.Path, asset.Path, StringComparison.Ordinal)
                    );
                
                    _assetsList.Add(new AssetCellVM(asset, previousIdenticalAsset));
                }
                
                Device.ExecuteOnMainThread(() => DisplayedAssetsList = new List<AssetCellVM>(_assetsList));
            });
        }
        
        private void DoSortByPath()
        {
            TaskEx.Run(() =>
            {
                SortByPath.CanExecute = false;
                _assetsList.Sort((a, b) => string.Compare(a.AssetPath, b.AssetPath, StringComparison.Ordinal));
                Device.ExecuteOnMainThread(() =>
                {
                    _displayedAssetsList = _assetsList.ToList();
                    SortByPath.CanExecute = true;
                });
            });
        }
    }
}