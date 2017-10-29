using WellFired.Guacamole.Data.Annotations;

namespace WellFired.Guacamole.DataBinding.Cells
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
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }
}