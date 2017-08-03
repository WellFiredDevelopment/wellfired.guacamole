using System.Collections;
using WellFired.Guacamole.Collection;
using WellFired.Guacamole.Data.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
    public partial class ItemsView
    {
        [PublicAPI] public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.Create<ItemsView, ICollection>(
                default(ICollection),
                BindingMode.TwoWay,
                viewBase => viewBase.ItemSource
            );

        [PublicAPI] public static readonly BindableProperty ItemTemplateProperty = BindableProperty
            .Create<ItemsView, DataTemplate>(
                null,
                BindingMode.TwoWay,
                viewBase => viewBase.ItemTemplate
            );

        [PublicAPI]
        public IList ItemSource
        {
            get { return (IList) GetValue(ItemSourceProperty); }
            set
            {
                var notifyCollectionChanged = ItemSource as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                    notifyCollectionChanged.CollectionChanged -= NotifyCollectionChangedOnCollectionChanged;

                SetValue(ItemSourceProperty, value);

                notifyCollectionChanged = value as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                    notifyCollectionChanged.CollectionChanged += NotifyCollectionChangedOnCollectionChanged;
                
                ItemSourceChanged();
            }
        }

        [PublicAPI]
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
    }
}