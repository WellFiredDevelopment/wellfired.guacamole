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
            // Here we simply build some mock data.
            var random = new Random(DateTime.Now.Millisecond);
            for (var n = 0; n < 100000; n++)
                _models.Add(new BuiltAssetData(BuiltAssetRandomizer.Create(random)));
            
            DisplayList = new List<BuiltAssetData>(_models);
            SortByPath = new Command { ExecuteAction = () => DoSortByPath() };
            SortByBefore = new Command { ExecuteAction = () => DoSortByBefore() };
            SortByAfter = new Command { ExecuteAction = () => DoSortByAfter() };
        }

        private void DoSortByPath()
        {
            // We do the sort in place because doing this and creating a list duplicate is faster than using OrderBy, and we have 100,000 entries
            _models.Sort((a, b) => string.Compare(a.Path, b.Path, StringComparison.Ordinal));
            DisplayList = _models.ToList();
        }

        private void DoSortByBefore()
        {
            // We do the sort in place because doing this and creating a list duplicate is faster than using OrderBy, and we have 100,000 entries
            _models.Sort((a, b) => a.BeforeSize.CompareTo(b.BeforeSize));
            DisplayList = _models.ToList();
        }

        private void DoSortByAfter()
        {
            // We do the sort in place because doing this and creating a list duplicate is faster than using OrderBy, and we have 100,000 entries
            _models.Sort((a, b) => a.AfterSize.CompareTo(b.AfterSize));
            DisplayList = _models.ToList();
        }
    }
}