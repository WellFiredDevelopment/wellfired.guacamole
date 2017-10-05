using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views.Cells;

namespace WellFired.Guacamole.Views.BindingContexts
{
    public class CellBindingContextBase : ObservableBase, IDefaultCellContext
    {
        private bool _isSelected;
        
        public CellBindingContextBase()
        {
            IsSelected = false;
        }

        [PublicAPI]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}