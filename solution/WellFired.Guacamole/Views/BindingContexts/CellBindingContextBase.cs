using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views.BindingContexts
{
    public class CellBindingContextBase : ObservableBase
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