using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding.Cells;

namespace WellFired.Guacamole.Examples.Intermediate.Sorting.ViewModel
{
    public class BuiltAssetData : CellBindingContextBase
    {
        private string _path;
        private int _beforeSize;
        private int _afterSize;

        [PublicAPI]
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }

        [PublicAPI]
        public int BeforeSize
        {
            get => _beforeSize;
            set => SetProperty(ref _beforeSize, value);
        }

        [PublicAPI]
        public int AfterSize
        {
            get => _afterSize;
            set => SetProperty(ref _afterSize, value);
        }

        public BuiltAssetData(Model.BuildAssetData model)
        {
            Path = model.Path;
            BeforeSize = model.BeforeSize;
            AfterSize = model.AfterSize;
        }
    }
}