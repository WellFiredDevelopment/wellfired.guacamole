using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Pages
{
    public partial class TabbedPage
    {
        [PublicAPI] public static readonly BindableProperty SelectedPageProperty = BindableProperty.Create<TabbedPage, object>(
            default(object),
            BindingMode.TwoWay,
            viewBase => viewBase.SelectedPage
        );

        [PublicAPI]
        public object SelectedPage
        {
            get => GetValue(SelectedPageProperty);
            set => SetValue(SelectedPageProperty, value);
        }
    }
}