using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Pages
{
    public partial class TabbedPage
    {
        [PublicAPI] public static readonly BindableProperty SelectedPageProperty = BindableProperty.Create<TabbedPage, object>(
            default(object),
            BindingMode.TwoWay,
            viewBase => viewBase.SelectedPage
        );

        /// <summary>
        /// Selected Page is the value of the backstore that should have corresponding tab view displayed. Note that the selected page
        /// should belong to the list of <see cref="ItemsView.ItemSource"/>.
        /// </summary>
        [PublicAPI]
        public object SelectedPage
        {
            get => GetValue(SelectedPageProperty);
            set => SetValue(SelectedPageProperty, value);
        }
    }
}