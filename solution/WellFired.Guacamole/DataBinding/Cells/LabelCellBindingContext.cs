using WellFired.Guacamole.Cells;
using JetBrains.Annotations;

namespace WellFired.Guacamole.DataBinding.Cells
{
    public class LabelCellBindingContext : CellBindingContextBase, IDefaultCellContext
    {
        private string _cellLabelText;

        [PublicAPI]
        public string CellLabelText
        {
            get => _cellLabelText;
            set => SetProperty(ref _cellLabelText, value);
        }

        public LabelCellBindingContext(string cellLabelText)
        {
            CellLabelText = cellLabelText;
        }
    }
}