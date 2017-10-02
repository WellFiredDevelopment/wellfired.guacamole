﻿using System;
using System.Collections;
using System.ComponentModel;
using WellFired.Guacamole.Collection;

namespace WellFired.Guacamole.Views
{
    /// <summary>
    /// This class takes care of the complexities of ItemsView, such as the ListView, here, we take care of ItemSources
    /// that may or may not be observable collections. we take the slightly complex notification system from .net and 
    /// whittle it down into easier to understand methods. (I hope).
    /// </summary>
    public abstract partial class ItemsView : ViewWithChildren, IItemsView
    {
        /// <summary>
        /// This is called when the whole ItemSource is changed. I.E. ItemSource = new collection();
        /// Note : This is only called if ItemSource is an ObservableCollection.
        /// </summary>
        protected abstract void ItemSourceChanged();
        
        /// <summary>
        /// This is called when the ItemSource is cleared. I.E. ItemSource.Clear();
        /// Note : This is only called if ItemSource is an ObservableCollection.
        /// </summary>
        protected abstract void ItemSourceCleared();
        
        /// <summary>
        /// This is called when a new Item is added to the ItemSource.
        /// Note : This is only called if ItemSource is an ObservableCollection.
        /// </summary>
        /// <param name="item">The new item</param>
        /// <param name="index">The new position this element was added at.</param>
        protected abstract void ItemAdded(object item, int index);
        
        /// <summary>
        /// This is called when an item is removed from the ItemSource
        /// Note : This is only called if ItemSource is an ObservableCollection.
        /// </summary>
        /// <param name="item">The removed Item</param>
        protected abstract void ItemRemoved(object item);
        
        /// <summary>
        /// This is called when an item is replaced within the ItemSource.
        /// Note : This is only called if ItemSource is an ObservableCollection.
        /// </summary>
        /// <param name="oldItem">The item that used to exist</param>
        /// <param name="newItem">The new item</param>
        /// <param name="index">The index into the ItemSource that you will find this item</param>
        protected abstract void ItemReplaced(object oldItem, object newItem, int index);

        private void AddCollection(IEnumerable items, int index)
        {
            foreach (var item in items)
            {
                ItemAdded(item, index);
                index++;
            }
        }

        private void RemoveCollection(IEnumerable items)
        {
            foreach (var item in items)
                ItemRemoved(item);
        }

        private void ReplaceCollection(IList oldItems, IList newItems, int index)
        {
            for (var n = 0; n < oldItems.Count; n++)
                ItemReplaced(oldItems[n], newItems[n], index + n);
        }

        private void ResetCollection()
        {
            ItemSourceCleared();
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

        protected override void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(sender, e);

            if (e.PropertyName == ItemSourceProperty.PropertyName)
            {
                if(ItemSource != null)
                    ItemSourceChanged();
                
                SetStyleDictionary(StyleDictionary);
            }
            else if (e.PropertyName == ItemTemplateProperty.PropertyName)
            {
                if(ItemSource != null)
                    ItemSourceChanged();
                
                SetStyleDictionary(StyleDictionary);
            }
        }
    }
}