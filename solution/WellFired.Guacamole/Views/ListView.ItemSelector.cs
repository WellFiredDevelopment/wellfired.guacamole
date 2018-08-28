using System;
using System.ComponentModel;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
	public class ItemSelector
	{
		private readonly IListView _listView;

		private ObservableCollection<INotifyPropertyChanged> _selectedItems;

		public ItemSelector(IListView listView)
		{
			_listView = listView;
		}

		public void SelectItem()
		{
			//ObservableCollection Clear() does not send event with removed items for performance reason. This is why we set elements
			//manually before to clear the list.
			foreach (INotifyPropertyChanged item in _listView.SelectedItems)
			{
				if (item is ISelectableCell selectableCell)
					selectableCell.IsSelected = false;
			}

			_listView.SelectedItems.Clear();

			if (_listView.SelectedItem == null)
				return;

			_listView.SelectedItems.Add(_listView.SelectedItem);

			if (_listView.SelectedItem is ISelectableCell selectedCell)
				selectedCell.IsSelected = true;
		}

		public void RegisterNewSelectedItems()
		{
			if (_listView.SelectedItems == null)
			{
				ResetSelectedItems();
				return;
			}

			if (_selectedItems != null)
			{
				foreach (INotifyPropertyChanged item in _selectedItems)
				{
					if (item is ISelectableCell cell)
						cell.IsSelected = false;
				}
			}

			_selectedItems = _listView.SelectedItems;

			foreach (INotifyPropertyChanged item in _listView.SelectedItems)
			{
				if (item is ISelectableCell cell)
					cell.IsSelected = true;
			}
		}

		public void ResetSelectedItems()
		{
			_listView.SelectedItems = new ObservableCollection<INotifyPropertyChanged>();
			_listView.SelectedItems.CollectionChanged += SelectedItems_OnCollectionChanged;
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

							_listView.OnItemSelected(_listView, new SelectedItemChangedEventArgs(item));
						}
					}

					if (e.OldItems != null)
					{
						foreach (var item in e.OldItems)
						{
							if (item is ISelectableCell cell)
								cell.IsSelected = false;
						}
					}

					break;
				case NotifyCollectionChangedAction.Move:
					break;
				case NotifyCollectionChangedAction.Reset:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}