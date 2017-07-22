using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views.BindingContexts;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.ViewModel
{
    public class Filter : CellBindingContextBase
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