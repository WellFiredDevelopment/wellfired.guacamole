using JetBrains.Annotations;
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
        
        [PublicAPI] public static readonly BindableProperty CanMouseOverProperty = BindableProperty.Create<Cell, bool>(
            true,
            BindingMode.TwoWay,
            v => v.CanMouseOver
        );
        
        [PublicAPI]
        public bool IsSelected
        {
            get => (bool) GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
        
        [PublicAPI]
        public bool CanMouseOver
        {
            get => (bool) GetValue(CanMouseOverProperty);
            set => SetValue(CanMouseOverProperty, value);
        }
    }
}