using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Pages
{
    public partial class TabbedPageButtonView
    {
        [PublicAPI] public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create<TabbedPageButtonView, bool>(
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