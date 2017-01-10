using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;

namespace WellFired.Guacamole.Views
{
    public partial class ItemsView
    {
        [PublicAPI] public static readonly BindableProperty ItemSourceProperty = BindableProperty
            .Create<ItemsView, IEnumerable>(
            null,
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
        public IEnumerable ItemSource
        {
            get { return (IEnumerable) GetValue(ItemSourceProperty); }
            set
            {
                var notifyCollectionChanged = ItemSource as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                    notifyCollectionChanged.CollectionChanged -= NotifyCollectionChangedOnCollectionChanged;

                SetValue(ItemSourceProperty, value);

                notifyCollectionChanged = value as INotifyCollectionChanged;
                if (notifyCollectionChanged != null)
                    notifyCollectionChanged.CollectionChanged += NotifyCollectionChangedOnCollectionChanged;

                BuildCollection();
            }
        }

        private static void NotifyCollectionChangedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            Logger.LogMessage($"CollectionChanged {notifyCollectionChangedEventArgs.NewItems[0]}");
        }

        [PublicAPI]
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
    }
}