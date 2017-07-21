using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views.BindingContexts
{
    public class StringCellBindingContext : ObservableBase
    {
        private string _cellLabelText;
        private bool _isSelected;

        [PublicAPI]
        public string CellLabelText
        {
            get { return _cellLabelText; }
            set { SetProperty(ref _cellLabelText, value); }
        }

        [PublicAPI]
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }

        public StringCellBindingContext(string cellLabelText)
        {
            CellLabelText = cellLabelText;
            IsSelected = false;
        }
    }
}