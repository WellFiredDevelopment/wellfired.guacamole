using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
	public class ItemSelector
	{
		private readonly IListView _listView;

		private ObservableCollection<INotifyPropertyChanged> _previousObservableCollection;
		
		
		/// <summary>
		/// ObservableCollection Clear() does not send event with removed items for performance reason to our CollectionChanged event.
		/// This is why we keep a reference to items that needs to be unselected. 
		/// </summary>
		private readonly List<INotifyPropertyChanged> _selectedItems = new List<INotifyPropertyChanged>();

		public ItemSelector(IListView listView)
		{
			_listView = listView;
		}

		private bool _itemBeingSelected;
		public void SelectItem()
		{
			_itemBeingSelected = true;
			_listView.SelectedItems?.Clear();
			_itemBeingSelected = false;

			if (_listView.SelectedItem == null)
				return;

			if (_listView.SelectedItems == null)
			{
				//Ensure you don't listen to the list here, the bindable object already take care of it when
				//this property change.
				_listView.SelectedItems = new ObservableCollection<INotifyPropertyChanged>();
			}
			
			_listView.SelectedItems.Add(_listView.SelectedItem);
		}

		/// <summary>
		/// Called when the observable collection of selected items is replaced by a new one
		/// </summary>
		public void RegisterNewSelectedItems()
		{
			UnselectPreviousItems();
			
			if (_previousObservableCollection != null)
			{
				_previousObservableCollection.CollectionChanged -= SelectedItems_OnCollectionChanged;
			}
			
			_previousObservableCollection = _listView.SelectedItems;

			if (_listView.SelectedItems == null)
				return;
			
			_listView.SelectedItems.CollectionChanged += SelectedItems_OnCollectionChanged;			
			
			CopySelectedItems();
			
			foreach (var item in _listView.SelectedItems)
			{
				if (item is ISelectableCell cell)
					cell.IsSelected = true;
			}
		}

		private void SelectedItems_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
				case NotifyCollectionChangedAction.Remove:
				case NotifyCollectionChangedAction.Replace:

					if (e.NewItems != null)
					{
						foreach (var item in e.NewItems)
						{
							if (item is ISelectableCell cell)
							{
								cell.IsSelected = true;
							}

							_selectedItems.Add((INotifyPropertyChanged) item);
							
							_listView.OnItemSelected(_listView, new SelectedItemChangedEventArgs(item));
						}
					}

					if (e.OldItems != null)
					{
						foreach (var item in e.OldItems)
						{
							if (item is ISelectableCell cell)
								cell.IsSelected = false;
							
							_selectedItems.Remove((INotifyPropertyChanged) item);
						}
					}

					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					UnselectPreviousItems();
					
					if (!_itemBeingSelected)
						_listView.SelectedItem = null;
					
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void CopySelectedItems()
		{
			_selectedItems.Clear();
			
			foreach (var item in _listView.SelectedItems)
			{
				_selectedItems.Add(item);
			}
		}

		private void UnselectPreviousItems()
		{
			foreach (var item in _selectedItems)
			{
				if (item is ISelectableCell cell)
					cell.IsSelected = false;
			}
			
			_selectedItems.Clear();
		}
	}
}