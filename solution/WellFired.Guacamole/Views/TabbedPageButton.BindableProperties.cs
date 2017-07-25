using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

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
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }
    }
}