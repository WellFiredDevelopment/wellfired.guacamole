using JetBrains.Annotations;
using WellFired.Guacamole.Views.BindingContexts;

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
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }

        [PublicAPI]
        public int BeforeSize
        {
            get { return _beforeSize; }
            set { SetProperty(ref _beforeSize, value); }
        }

        [PublicAPI]
        public int AfterSize
        {
            get { return _afterSize; }
            set { SetProperty(ref _afterSize, value); }
        }

        public BuiltAssetData(Model.BuildAssetData model)
        {
            Path = model.Path;
            BeforeSize = model.BeforeSize;
            AfterSize = model.AfterSize;
        }
    }
}