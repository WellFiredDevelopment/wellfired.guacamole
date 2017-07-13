using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Examples.Taskist.ViewModel
{
    public class Filter : ObservableBase
    {
        private string _filterName;
        private UIColor _filterColor;

        public string FilterName
        {
            get { return _filterName; }
            set { SetProperty(ref _filterName, value); }
        }

        public UIColor FilterColor
        {
            get { return _filterColor; }
            set { SetProperty(ref _filterColor, value); }
        }
    }
}