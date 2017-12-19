using System.Collections;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
    public partial class ItemsView
    {
        [PublicAPI] public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create<ItemsView, ICollection>(default(ICollection), BindingMode.TwoWay, viewBase => viewBase.ItemSource);
        [PublicAPI] public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create<ItemsView, DataTemplate>(null, BindingMode.TwoWay, viewBase => viewBase.ItemTemplate);
        [PublicAPI] public static readonly BindableProperty HeaderTemplateProperty = BindableProperty.Create<ItemsView, DataTemplate>(DataTemplate.Of(typeof(HeaderCell)), BindingMode.TwoWay, viewBase => viewBase.HeaderTemplate);

        protected CompositeCollection CompositeCollection = new CompositeCollection();
        
        [PublicAPI]
        public IList ItemSource
        {
            get => (IList) GetValue(ItemSourceProperty);
            set
            {
                SetValue(ItemSourceProperty, value);
                RegisterNewItemSource();
            }
        }

        [PublicAPI]
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate) GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        [PublicAPI]
        public DataTemplate HeaderTemplate
        {
            get => (DataTemplate) GetValue(HeaderTemplateProperty);
            set => SetValue(HeaderTemplateProperty, value);
        }
    }
}