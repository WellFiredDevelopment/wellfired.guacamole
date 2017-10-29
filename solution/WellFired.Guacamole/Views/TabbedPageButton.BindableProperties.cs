using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
    public partial class TabbedPageButton
    {
        [PublicAPI] public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create<TabbedPageButton, bool>(
            default(bool),
            BindingMode.TwoWay,
            v => v.IsSelected
        );

        [PublicAPI]
        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
    }
}