using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.Collection;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Layouts;

namespace WellFired.Guacamole.Views
{
    public partial class ItemsView
    {
        [PublicAPI] public static readonly BindableProperty ChildrenProperty = BindableProperty.Create<ItemsView, IList<ILayoutable>>(default(IList<ILayoutable>),
            BindingMode.TwoWay,
            v => v.Children
        );

        [PublicAPI]
        public IList<ILayoutable> Children
        {
            get { return (IList<ILayoutable>) GetValue(ChildrenProperty); }
            set { SetValue(ChildrenProperty, value); }
        }

        [PublicAPI] public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.Create<ItemsView, IEnumerable>(
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

        [PublicAPI]
        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate) GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        private void NotifyCollectionChangedOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyEvent)
        {
            switch (notifyEvent.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddCollection(notifyEvent.NewItems, notifyEvent.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RemoveCollection(notifyEvent.OldItems);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    ReplaceCollection(notifyEvent.OldItems, notifyEvent.NewItems, notifyEvent.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Move:
                    RemoveCollection(notifyEvent.OldItems);
                    AddCollection(notifyEvent.OldItems, notifyEvent.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    ResetCollection();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}