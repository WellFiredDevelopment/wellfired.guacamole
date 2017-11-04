using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Utilities;
using WellFired.Guacamole.Platform;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
	public class AssetsBase : ObservableBase
	{
		private enum State
		{
			Unselected = 0,
			Ordered = 1,
			Reversed = 2
		}
        
		private const string InitialAssetPathText = "Asset Path";
		private const string InitialImportedSizeText = "Imported Size";
		private const string InitialRawSizeText = "Raw Size";
		private const string InitialPercentageText = "Percentage";
        
		private List<AssetCell> _assetsList;
		private string _totalSize;
		private UIColor _totalSizeBackgroundColor;
		private IList<AssetCell> _displayedAssetsList;
		private Command _sortByAssetPath;
		private Command _sortByImportedSize;
		private Command _sortByRawSize;
		private Command _sortByPercentage;
		private State _assetPathState;
		private State _importedSizeState;
		private State _rawSizeState;
		private State _percentageState;
		private string _assetPathText = InitialAssetPathText;
		private string _importedSizeText = InitialImportedSizeText;
		private string _rawSizeText = InitialRawSizeText;
		private string _percentageText = InitialPercentageText;

		[PublicAPI]
		public IList<AssetCell> DisplayedAssetsList
		{
			get => _displayedAssetsList;
			set => SetProperty(ref _displayedAssetsList, value);
		}
        
		[PublicAPI]
		public string TotalSize
		{
			get => _totalSize;
			set => SetProperty(ref _totalSize, value);
		}

		[PublicAPI]
		public UIColor TotalSizeBacgroundColor
		{
			get => _totalSizeBackgroundColor;
			set => SetProperty(ref _totalSizeBackgroundColor, value);
		}
        
		public string AssetPathText 
		{ 
			get => _assetPathText;
			set => SetProperty(ref _assetPathText, value);
		}
        
		public string ImportedSizeText 
		{ 
			get => _importedSizeText;
			set => SetProperty(ref _importedSizeText, value);
		}
        
		public string RawSizeText 
		{ 
			get => _rawSizeText;
			set => SetProperty(ref _rawSizeText, value);
		}
        
		public string PercentageText 
		{ 
			get => _percentageText;
			set => SetProperty(ref _percentageText, value);
		}

		[PublicAPI]
		public Command SortByAssetPath
		{
			get => _sortByAssetPath;
			set => SetProperty(ref _sortByAssetPath, value);
		}
        
		[PublicAPI]
		public Command SortByImportedSize
		{
			get => _sortByImportedSize;
			set => SetProperty(ref _sortByImportedSize, value);
		}
        
		[PublicAPI]
		public Command SortByRawSize
		{
			get => _sortByRawSize;
			set => SetProperty(ref _sortByRawSize, value);
		}
        
		[PublicAPI]
		public Command SortByPercentage
		{
			get => _sortByPercentage;
			set => SetProperty(ref _sortByPercentage, value);
		}

		public AssetsBase(List<IAsset> assets, List<IAsset> previousAssets)
		{
			GenerateTotalSize(assets, previousAssets);
			GenerateAssetsList(assets, previousAssets);
            
			SortByAssetPath = new Command { ExecuteAction = () => {
				PerformSort(ref _assetPathState, a => a.AssetPath);
				AssetPathText = GetPath(InitialAssetPathText, _assetPathState);
			}};
			SortByImportedSize = new Command { ExecuteAction = () => {
				PerformSort(ref _importedSizeState, a => a.ImportedSize);
				ImportedSizeText = GetPath(InitialImportedSizeText, _importedSizeState);
			}};
			SortByRawSize = new Command { ExecuteAction = () => {
				PerformSort(ref _rawSizeState, a => a.RawSize);
				RawSizeText = GetPath(InitialRawSizeText, _rawSizeState);
			}};
			SortByPercentage = new Command { ExecuteAction = () => {
				PerformSort(ref _percentageState, a => a.Percentage);
				PercentageText = GetPath(InitialPercentageText, _percentageState);
			}};
		}

		private void PerformSort<TKey>(ref State state, Func<AssetCell, TKey> keySelector)
		{
			var cachedState = state;
			Reset();
			state = CycleState(cachedState);
			DoSort(state, keySelector);
		}

		private void Reset()
		{
			_assetPathState = State.Unselected;
			AssetPathText = GetPath(InitialAssetPathText, _assetPathState);
			_importedSizeState = State.Unselected;
			ImportedSizeText = GetPath(InitialImportedSizeText, _importedSizeState);
			_rawSizeState = State.Unselected;
			RawSizeText = GetPath(InitialRawSizeText, _rawSizeState);
			_percentageState = State.Unselected;
			PercentageText = GetPath(InitialPercentageText, _percentageState);
		}

		private void GenerateTotalSize(IEnumerable<IAsset> assets, List<IAsset> previousAssets = null)
		{
			var size = new FileSize(0f);
			size = assets.Aggregate(size, (current, asset) => current + asset.ImportedSize);
			TotalSize = $"{size.SizeInMb:0.00} MB";

			if (previousAssets == null) 
				return;
            
			var previousSize = new FileSize(0f);
			previousSize = previousAssets.Aggregate(previousSize, (current, asset) => current + asset.ImportedSize);
                
			TotalSizeBacgroundColor = ColorCompare.CompareSizeColor(size, previousSize);
		}

		private void GenerateAssetsList(IEnumerable<IAsset> assets, List<IAsset> previouslyAssets = null)
		{
			_assetsList = new List<AssetCell>();
            
			foreach (var asset in assets)
			{
				var previousIdenticalAsset = previouslyAssets?.Find(x => string.Equals(x.Path, asset.Path, StringComparison.Ordinal));
				_assetsList.Add(new AssetCell(asset, previousIdenticalAsset));
			}

			DisplayedAssetsList = _assetsList;
		}

		private void DoSort<TKey>(State state, Func<AssetCell, TKey> keySelector)
		{
			TaskEx.Run(() => {
				MainThreadRunner.ExecuteOnMainThread(() =>
				{
					switch (state)
					{
						case State.Unselected:
							break;
						case State.Ordered:
							DisplayedAssetsList = _assetsList.OrderBy(keySelector).ToList();
							break;
						default:
							DisplayedAssetsList = _assetsList.OrderByDescending(keySelector).ToList();
							break;
					}
				});
			});   
		}

		private State CycleState(State assetPathState)
		{
			switch (assetPathState)
			{
				case State.Unselected:
					return State.Ordered;
				case State.Ordered:
					return State.Reversed;
				case State.Reversed:
					return State.Ordered;
				default:
					throw new ArgumentOutOfRangeException(nameof(assetPathState), assetPathState, null);
			}
		}

		private string GetPath(string path, State assetPathState)
		{
			switch (assetPathState)
			{
				case State.Unselected:
					return path;
				case State.Ordered:
					return path + "↓";
				case State.Reversed:
					return path + "↑";
				default:
					throw new ArgumentOutOfRangeException(nameof(assetPathState), assetPathState, null);
			}
		}
	}
}