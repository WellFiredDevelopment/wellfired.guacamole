using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Cells
{
    public partial class Cell
    {
        [PublicAPI] public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create<Cell, bool>(
            false,
            BindingMode.TwoWay,
            v => v.IsSelected
        );
        
        [PublicAPI]
        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
    }
}