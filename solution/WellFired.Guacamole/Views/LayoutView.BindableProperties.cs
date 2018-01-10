using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class LayoutView
    {   
        [PublicAPI] public static readonly BindableProperty LayoutProperty = BindableProperty.Create<LayoutView, ILayoutChildren>(
            default(ILayoutChildren),
            BindingMode.TwoWay,
            layoutView => layoutView.Layout
        );

        [PublicAPI]
        public ILayoutChildren Layout
        {
            get => (ILayoutChildren) GetValue(LayoutProperty);
            set => SetValue(LayoutProperty, value);
        }
    }
}