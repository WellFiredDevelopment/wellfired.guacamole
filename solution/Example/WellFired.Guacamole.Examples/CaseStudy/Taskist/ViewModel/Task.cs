using WellFired.Guacamole.DataBinding.Cells;

namespace WellFired.Guacamole.Examples.CaseStudy.Taskist.ViewModel
{
    public class Task : CellBindingContextBase
    {
        private string _description;
        private bool _done;

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public bool Done
        {
            get => _done;
            set => SetProperty(ref _done, value);
        }
    }
}