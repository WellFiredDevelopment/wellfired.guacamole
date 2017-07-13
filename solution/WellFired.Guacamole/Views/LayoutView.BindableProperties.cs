using System.Collections.Generic;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class LayoutView
    {
        [PublicAPI] public static readonly BindableProperty ChildrenProperty = BindableProperty.Create<LayoutView, IList<ILayoutable>>(
            default(IList<ILayoutable>),
            BindingMode.TwoWay,
            layoutView => layoutView.Children
        );
        
        [PublicAPI] public static readonly BindableProperty LayoutProperty = BindableProperty.Create<LayoutView, ILayoutChildren>(
            default(ILayoutChildren),
            BindingMode.TwoWay,
            layoutView => layoutView.Layout
        );

        [PublicAPI]
        public IList<ILayoutable> Children
        {
            get { return (IList<ILayoutable>) GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        [PublicAPI]
        public ILayoutChildren Layout
        {
            get { return (ILayoutChildren) GetValue(LayoutProperty); }
            set { SetValue(LayoutProperty, value); }
        }
    }
}