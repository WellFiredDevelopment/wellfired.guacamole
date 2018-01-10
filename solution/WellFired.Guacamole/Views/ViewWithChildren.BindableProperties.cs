using System.Collections.Generic;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public abstract partial class ViewWithChildren
    {
        [PublicAPI] public static readonly BindableProperty ChildrenProperty = BindableProperty.Create<ItemsView, IList<ILayoutable>>(default(IList<ILayoutable>),
            BindingMode.TwoWay,
            v => v.Children
        );

        [PublicAPI]
        public IList<ILayoutable> Children
        {
            get => (IList<ILayoutable>) GetValue(ChildrenProperty);
            set => SetValue(ChildrenProperty, value);
        }
    }
}