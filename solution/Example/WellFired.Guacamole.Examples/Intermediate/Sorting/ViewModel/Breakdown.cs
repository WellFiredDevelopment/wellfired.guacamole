using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Examples.Intermediate.Sorting.Helper;

namespace WellFired.Guacamole.Examples.Intermediate.Sorting.ViewModel
{
    public class Breakdown : CellBindingContextBase
    {
        private readonly List<BuiltAssetData> _models = new List<BuiltAssetData>();

        private Command _sortByPath;
        private Command _sortByBefore;
        private Command _sortByAfter;
        private List<BuiltAssetData> _displayList = new List<BuiltAssetData>();

        [PublicAPI]
        public List<BuiltAssetData> DisplayList
        {
            get { return _displayList; }
            set { SetProperty(ref _displayList, value); }
        }
        
        [PublicAPI]
        public Command SortByPath
        {
            get { return _sortByPath; }
            set { SetProperty(ref _sortByPath, value); }
        }
        
        [PublicAPI]
        public Command SortByBefore
        {
            get { return _sortByBefore; }
            set { SetProperty(ref _sortByBefore, value); }
        }
        
        [PublicAPI]
        public Command SortByAfter
        {
            get { return _sortByAfter; }
            set { SetProperty(ref _sortByAfter, value); }
        }

        public Breakdown()
        {
            TaskEx.Run(() =>
            {
                // Here we simply build some mock data.
                var random = new Random(DateTime.Now.Millisecond);
                for (var n = 0; n < 100000; n++)
                    _models.Add(new BuiltAssetData(BuiltAssetRandomizer.Create(random)));
                
                Device.ExecuteOnMainThread(() => DisplayList = new List<BuiltAssetData>(_models));
            });
            
            DisplayList = new List<BuiltAssetData>();
            SortByPath = new Command { ExecuteAction = () => DoSortByPath() };
            SortByBefore = new Command { ExecuteAction = () => DoSortByBefore() };
            SortByAfter = new Command { ExecuteAction = () => DoSortByAfter() };
        }

        private void DoSortByPath()
        {
            TaskEx.Run(() =>
            {
                SortByPath.CanExecute = false;
                _models.Sort((a, b) => string.Compare(a.Path, b.Path, StringComparison.Ordinal));
                Device.ExecuteOnMainThread(() =>
                {
                    DisplayList = _models.ToList();
                    SortByPath.CanExecute = true;
                });
            });
        }

        private void DoSortByBefore()
        {
            TaskEx.Run(() =>
            {
                SortByBefore.CanExecute = false;
                _models.Sort((a, b) => a.BeforeSize.CompareTo(b.BeforeSize));
                Device.ExecuteOnMainThread(() =>
                {
                    DisplayList = _models.ToList();
                    SortByBefore.CanExecute = true;
                });
            });
        }

        private void DoSortByAfter()
        {
            TaskEx.Run(() =>
            {
                SortByAfter.CanExecute = false;
                _models.Sort((a, b) => a.AfterSize.CompareTo(b.AfterSize));
                Device.ExecuteOnMainThread(() =>
                {
                    DisplayList = _models.ToList();
                    SortByAfter.CanExecute = true;
                });
            });
        }
    }
}