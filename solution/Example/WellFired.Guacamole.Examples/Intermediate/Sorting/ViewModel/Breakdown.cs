using System;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Collection;
using WellFired.Guacamole.Examples.Intermediate.Sorting.Helper;
using WellFired.Guacamole.Views.BindingContexts;

namespace WellFired.Guacamole.Examples.Intermediate.Sorting.ViewModel
{
    public class Breakdown : CellBindingContextBase
    {
        private readonly List<BuiltAssetData> _models = new List<BuiltAssetData>();

        private Command _sortByPath;
        private Command _sortByBefore;
        private Command _sortByAfter;
        private ObservableCollection<BuiltAssetData> _displayList = new ObservableCollection<BuiltAssetData>();

        [PublicAPI]
        public ObservableCollection<BuiltAssetData> DisplayList
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
            // Here we simply build some mock data.
            var random = new Random(DateTime.Now.Millisecond);
            for (var n = 0; n < 100; n++)
                _models.Add(new BuiltAssetData(BuiltAssetRandomizer.Create(random)));
            
            DisplayList = new ObservableCollection<BuiltAssetData>(_models);
            SortByPath = new Command { ExecuteAction = () => DoSortByPath() };
            SortByBefore = new Command { ExecuteAction = () => DoSortByBefore() };
            SortByAfter = new Command { ExecuteAction = () => DoSortByAfter() };
        }

        private void DoSortByPath()
        {
            DisplayList = new ObservableCollection<BuiltAssetData>(_models.OrderBy(o => o.Path).ToList());
        }

        private void DoSortByBefore()
        {
            DisplayList = new ObservableCollection<BuiltAssetData>(_models.OrderBy(o => o.BeforeSize).ToList());
        }

        private void DoSortByAfter()
        {
            DisplayList = new ObservableCollection<BuiltAssetData>(_models.OrderBy(o => o.AfterSize).ToList());
        }
    }
}