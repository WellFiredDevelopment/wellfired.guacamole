using WellFired.Guacamole.Annotations;

namespace WellFired.Guacamole.Views.BindingContexts
{
    public class LabelCellBindingContext : CellBindingContextBase
    {
        private string _cellLabelText;

        [PublicAPI]
        public string CellLabelText
        {
            get { return _cellLabelText; }
            set { SetProperty(ref _cellLabelText, value); }
        }

        public LabelCellBindingContext(string cellLabelText)
        {
            CellLabelText = cellLabelText;
        }
    }
}