using WellFired.Guacamole.DataBinding.Cells;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.ViewModel
{
    public class Task : CellBindingContextBase
    {
        private string _description;
        private bool _done;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public bool Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }
    }
}