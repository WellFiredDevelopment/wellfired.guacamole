using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.Views.Cells;

namespace WellFired.Guacamole.Views.BindingContexts
{
    public class LabelCellBindingContext : CellBindingContextBase, IDefaultCellContext
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