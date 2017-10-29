using System.Collections;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
    public partial class ItemsView
    {
        [PublicAPI] public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create<ItemsView, ICollection>(default(ICollection), BindingMode.TwoWay, viewBase => viewBase.ItemSource);
        [PublicAPI] public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create<ItemsView, DataTemplate>(null, BindingMode.TwoWay, viewBase => viewBase.ItemTemplate);
        [PublicAPI] public static readonly BindableProperty HeaderTemplateProperty = BindableProperty.Create<ItemsView, DataTemplate>(null, BindingMode.TwoWay, viewBase => viewBase.HeaderTemplate);

        private CompositeCollection _compositeCollection = new CompositeCollection();
        
        [PublicAPI]
        public IList ItemSource
        {
            get => (IList) GetValue(ItemSourceProperty);
            set
            {
                _compositeCollection.CollectionChanged -= NotifyCollectionChangedOnCollectionChanged;

                SetValue(ItemSourceProperty, value);

                // We internally build a CompositeCollection to house our entries, this allows us to provide complex NotifyCollectionChanged behavious with multiple collections.
                _compositeCollection = new CompositeCollection(ItemSource);
                _compositeCollection.CollectionChanged += NotifyCollectionChangedOnCollectionChanged;
                
                ItemSourceChanged();
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