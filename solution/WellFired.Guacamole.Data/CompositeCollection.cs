using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WellFired.Guacamole.Data.Collection;

namespace WellFired.Guacamole.Data
{
	/// <summary>
	/// This class is a representation of a two tiered List of Lists. The parent list could be an ObservableCollection, as could any or all
	/// of the children. This class allows contiguous access to a list of lists or ObservableCollection of ObservableCollection.
	/// The class also implements INotifyCollectionChanged and as such can be used as in the same way as an ObservableCollection.
	/// </summary>
	public class CompositeCollection : INotifyCollectionChanged, IEnumerable
	{
		private ArrayList _itemSource;
		private bool _isGroupingEnabled;

		/// <summary>
		/// Group mapping is only valid when we have _isGroupingEnabled turned on. Group mapping allows us to quickly map from a group index to a linear 
		/// index, given, for example the following data
		/// i : 0		g : 0
		/// 	i : 1
		/// 	i : 2
		/// i : 3		g : 1
		/// 	i : 4
		/// 	i : 5
		/// 	i : 6
		/// 	i : 7
		/// i : 8		g : 2
		/// Our Group mapping would contain quick mapping index between g 2 and index 8
		/// </summary>
		private readonly Dictionary<int, int> _groupToIndexMapping = new Dictionary<int, int>(); 
		
		/// <summary>
		/// The enumerator for this data type simply returns our internal representation
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator() => _itemSource.GetEnumerator();
		
		/// <summary>
		/// This flas will tell you if the ItemSource is a grouped ItemSource (I.E. Not contiguous)
		/// </summary>
		public bool IsContiguousCollection => !_isGroupingEnabled;

		/// <summary>
		/// Constructs a new instance of TwoTieredCollection from a List. This list can be an observable Collection, it's children can also be 
		/// ObservableCollection.
		/// </summary>
		public CompositeCollection(ICollection itemSource)
		{
			BuildFrom(itemSource);
		}

		public CompositeCollection()
		{
			_itemSource = new ArrayList();
		}

		/// <summary>
		/// This builds our internal representation of our DataStructure, this method will also throw away the previouse itemSource, so be sure
		/// you've unsubscribed any necesary events.
		/// </summary>
		/// <param name="itemSource"></param>
		private void BuildFrom(ICollection itemSource)
		{
			if (!itemSource.GetType().GetGenericArguments().Any())
			{
				_isGroupingEnabled = false;
				_itemSource = new ArrayList(itemSource);
				return;
			}
			
			var genericType = itemSource.GetType().GetGenericArguments()[0];
			_isGroupingEnabled = typeof(ICollection).IsAssignableFrom(genericType);

			if (itemSource is INotifyCollectionChanged notifyCollectionChanged)
				notifyCollectionChanged.CollectionChanged += MainCollectionChanged;

			// With grouping disabled we do a simple list copy, referencing the source raw.
			if (!_isGroupingEnabled)
			{
				_itemSource = new ArrayList(itemSource);
				return;
			}
			
			// If grouping is enabled, we build one larger list from the source, inserting data so that it is linear.
			// We also register any needed INotifyCollectionChanged
			_itemSource = new ArrayList();
			
			// Build our internal list
			foreach (var item in itemSource)
				AddItemAndChildrenToInternal((ICollection)item, _itemSource.Count);
			
			// Calculate our group mapping
			CalculateGroupMapping(itemSource);
		}

		/// <summary>
		/// This method builds us a quick mapping from group Id to contiguous index.
		/// </summary>
		/// <param name="itemSource"></param>
		private void CalculateGroupMapping(ICollection itemSource)
		{
			_groupToIndexMapping.Clear();
			var runningTotal = 0;
			var count = 0;
			foreach (var unused in itemSource)
			{
				_groupToIndexMapping[count] = runningTotal;

				if(unused is ICollection subcollection)
					runningTotal += subcollection.Count;
				
				runningTotal += 1;
				count++;
			}
		}

		/// <summary>
		/// This will remove an item that should be an IEnumerable and all of it's children from the single level list.
		/// The method will return the number of removed entries
		/// </summary>
		/// <param name="collection">The collection we'd like to remove</param>
		/// <param name="startingIndex">The index at which we should start the removal</param>
		/// <exception cref="Exception"></exception>
		private void RemoveItemAndChildrenFromInternal(ICollection collection, int startingIndex)
		{
			if(collection == null)
				throw new Exception("The item you're removing should implement IEnumerable");
			
			if(!_isGroupingEnabled)
				throw new Exception("Should not be removing item with children if grouping is not enabled");
				
			if(collection is INotifyCollectionChanged subNotifyCollectionChanged)
				subNotifyCollectionChanged.CollectionChanged -= SubCollectionChanged;

			var endingIndex = startingIndex + collection.Count;
			for (var removalIndex = endingIndex; removalIndex >= startingIndex; removalIndex--)
				_itemSource.RemoveAt(removalIndex);
		}

		/// <summary>
		/// This will add an item that should be an IEnumerable and all of it's children to the single level list.
		/// The method will start inserting at insertionIndex and will increment insertionIndex for every item that 
		/// get's added, returning the new value.
		/// </summary>
		/// <param name="collection"></param>
		/// <param name="insertionIndex"></param>
		/// <exception cref="Exception"></exception>
		private int AddItemAndChildrenToInternal(IEnumerable collection, int insertionIndex)
		{
			if(collection == null)
				throw new Exception("The item you're adding should implement IEnumerable");
			
			if(!_isGroupingEnabled)
				throw new Exception("Should not be adding item with children if grouping is not enabled");
				
			if(collection is INotifyCollectionChanged subNotifyCollectionChanged)
				subNotifyCollectionChanged.CollectionChanged += SubCollectionChanged;

			_itemSource.Insert(insertionIndex, collection);
			insertionIndex++;

			foreach (var subItem in collection)
			{
				_itemSource.Insert(insertionIndex, subItem);
				insertionIndex++;
			}

			return insertionIndex;
		}

		/// <summary>
		/// Takes a Collection of items (A root Grouped collection and appends each entry to one arrayList.)
		/// </summary>
		/// <param name="collection"></param>
		/// <param name="items"></param>
		/// <exception cref="Exception"></exception>
		private void AddItemAndChildren(IEnumerable collection, ref ArrayList items)
		{
			if(collection == null)
				throw new Exception("The item you're adding should implement IEnumerable");
			
			if(!_isGroupingEnabled)
				throw new Exception("Should not be adding item with children if grouping is not enabled");

			items.Add(collection);
			foreach (var subItem in collection)
				items.Add(subItem);
		}

		/// <summary>
		/// Provides array index to a one or two tiered data structure, as though the data structure was linear.
		/// </summary>
		/// <param name="i"></param>
		public object this[int i] => _itemSource[i];

		/// <summary>
		/// Returns the total count of this collection as though it was linear
		/// </summary>
		public int Count => _itemSource.Count;
		
		/// <summary>
		/// One of our Top Tier collections has changed, we handle this differently if we have grouping enabled or not. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		private void MainCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (!_isGroupingEnabled)
				MainCollectionChangedNoGrouping(e);
			else
				MainCollectionChangedGrouping(sender, e);
		}

		/// <summary>
		/// Our internal representation of our collection changing without grouping.
		/// We handle this simply ourself, then we pass the message along to anyone 
		/// listening.
		/// </summary>
		/// <param name="e"></param>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		private void MainCollectionChangedNoGrouping(NotifyCollectionChangedEventArgs e)
		{	
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					for (var index = 0; index < e.NewItems.Count; index++)
						_itemSource.Insert(e.NewStartingIndex + index, e.NewItems[index]);
					break;
				case NotifyCollectionChangedAction.Remove:
				{
					var startIndex = e.OldStartingIndex;
					var endIndex = startIndex + e.OldItems.Count - 1;
					for (; endIndex >= startIndex; endIndex--)
						_itemSource.RemoveAt(endIndex);
					break;
				}
				case NotifyCollectionChangedAction.Replace:
					for (var n = 0; n < e.OldItems.Count; n++)
						_itemSource[e.NewStartingIndex + n] = e.NewItems[n];
					break;
				case NotifyCollectionChangedAction.Move:
				{
					var startIndex = e.OldStartingIndex;
					var endIndex = startIndex + e.OldItems.Count - 1;
					for (; endIndex >= startIndex; endIndex--)
						_itemSource.RemoveAt(endIndex);
					
					for (var index = 0; index < e.NewItems.Count; index++)
						_itemSource.Insert(e.NewStartingIndex + index, e.NewItems[index]);
					break;
				}
				case NotifyCollectionChangedAction.Reset:
					ClearAllDelegates();
					_itemSource.Clear();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			// Now we've updated our internal representation, we pass this message along to anyone who cares.
			CollectionChanged?.Invoke(this, e);
		}

		private void MainCollectionChangedGrouping(object sender, NotifyCollectionChangedEventArgs e)
		{
			var data = new List<NotifyCollectionChangedEventArgs>();
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
				{
					// We test the group here to find out if this is being inserted over an existing entry,
					// if the corresponding entry does not exist in our mapping, we're adding a new item. 
					var isAdd = _groupToIndexMapping.ContainsKey(e.NewStartingIndex);
					var startingIndex = isAdd ? _groupToIndexMapping[e.NewStartingIndex] : _itemSource.Count;
					var count = startingIndex;
					
					// Insert the items in their rightful position
					foreach (var item in e.NewItems)
						count += AddItemAndChildrenToInternal((ICollection) item, count);

					var items = new ArrayList();
					foreach (var item in e.NewItems)
						AddItemAndChildren((ICollection) item, ref items);
					
					data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items, startingIndex));
					break;
				}
				case NotifyCollectionChangedAction.Remove:
				{
					var startingIndex = _groupToIndexMapping[e.OldStartingIndex];
					foreach (var item in e.OldItems)
						RemoveItemAndChildrenFromInternal((ICollection) item, startingIndex);

					var items = new ArrayList();
					foreach (var item in e.OldItems)
						AddItemAndChildren((ICollection) item, ref items);
					
					data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items, startingIndex));
					break;
				}
				case NotifyCollectionChangedAction.Move:
				case NotifyCollectionChangedAction.Replace:
				{
					var startingIndex = _groupToIndexMapping[e.NewStartingIndex];
					
					foreach (var item in e.OldItems)
						RemoveItemAndChildrenFromInternal((ICollection)item, startingIndex);
					
					foreach (var item in e.NewItems)
						AddItemAndChildrenToInternal((ICollection)item, startingIndex);

					var oldItems = new ArrayList();
					foreach (var item in e.OldItems)
						AddItemAndChildren((ICollection) item, ref oldItems);
					
					var newItems = new ArrayList();
					foreach (var item in e.NewItems)
						AddItemAndChildren((ICollection) item, ref newItems);

					// If we've removed and added a different number of items, this cannot be a simple move or replace. Instead, we 
					// build a seperate remove and add step so all the items are synch'd
					if (oldItems.Count != newItems.Count)
					{
						data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, oldItems, startingIndex));
						data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems, startingIndex));
					}
					else
					{
						// In this case, the action could be replace or move, so we pass it directly down.
						data.Add(new NotifyCollectionChangedEventArgs(e.Action, newItems, oldItems, startingIndex));	
					}
					
					break;
				}
				case NotifyCollectionChangedAction.Reset:
					ClearAllDelegates();
					_itemSource.Clear();
					data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			CalculateGroupMapping((ICollection)sender);
			data.ForEach(o => CollectionChanged?.Invoke(this, o));
		}

		/// <summary>
		/// This is called when one of the sub collections has changed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <exception cref="Exception"></exception>
		private void SubCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if(!_isGroupingEnabled)
				throw new Exception("Sub Collection should not be modified when _isGroupingEnabled == false");
			
			var data = new List<NotifyCollectionChangedEventArgs>();
			var senderIndex = _itemSource.IndexOf(sender); 
			
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					for (var index = 0; index < e.NewItems.Count; index++)
						_itemSource.Insert(senderIndex + e.NewStartingIndex + index + 1, e.NewItems[index]);
					data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, e.NewItems, senderIndex + 1 + e.NewStartingIndex));
					break;
				case NotifyCollectionChangedAction.Remove:
				{
					var startIndex = senderIndex + e.OldStartingIndex + 1;
					var endIndex = startIndex + e.OldItems.Count - 1;
					for (; endIndex >= startIndex; endIndex--)
						_itemSource.RemoveAt(endIndex);
					
					data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, e.OldItems, startIndex));
					break;
				}
				case NotifyCollectionChangedAction.Replace:
					for (var n = 0; n < e.OldItems.Count; n++)
						_itemSource[senderIndex + 1 + e.NewStartingIndex + n] = e.NewItems[n];
					
					data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, e.NewItems, e.OldItems, senderIndex + 1 + e.NewStartingIndex));
					break;
				case NotifyCollectionChangedAction.Move:
				{
					var startIndex = senderIndex + 1 + e.OldStartingIndex;
					var endIndex = startIndex + e.OldItems.Count - 1;
					for (; endIndex >= startIndex; endIndex--)
						_itemSource.RemoveAt(endIndex);

					var newStartingIndex = senderIndex + 1 + e.NewStartingIndex;
					for (var index = 0; index < e.NewItems.Count; index++)
						_itemSource.Insert(newStartingIndex + index, e.NewItems[index]);
					
					data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, e.NewItems, newStartingIndex, startIndex));
					break;
				}
				case NotifyCollectionChangedAction.Reset:
				{
					var startIndex = senderIndex;
					var sourceCollection = (ICollection)_itemSource[startIndex];
					data.Add(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new ArrayList(sourceCollection), startIndex));
					break;
				}
				default:
					throw new ArgumentOutOfRangeException();
			}
			
			CalculateGroupMapping((ICollection)sender);
			data.ForEach(o => CollectionChanged?.Invoke(this, o));
		}

		/// <summary>
		/// Removes all delegates from all objects that might be registered.
		/// </summary>
		private void ClearAllDelegates()
		{
			foreach (var item in _itemSource)
			{
				if (item is INotifyCollectionChanged subNotifyCollectionChanged)
					subNotifyCollectionChanged.CollectionChanged -= SubCollectionChanged;
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged;

		/// <summary>
		/// Returns the index of the passed item
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public int IndexOf(object item)
		{
			return _itemSource.IndexOf(item);
		}
	}
}