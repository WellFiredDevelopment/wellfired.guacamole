using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Diagnostics;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Platforms;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.Simple.ListViewWithBoundDataExample
{
	public class BoundObject : ObservableBase
	{
		private ObservableCollection<INotifyPropertyChanged> _selectedItems;

		public IList Data => ItemSource.From("First Sausage", "Second Sausage", "Third Sausage");

		public ObservableCollection<INotifyPropertyChanged> SelectedItems
		{
			get => _selectedItems;
			set => SetProperty(ref _selectedItems, value);
		}

	}
	
	public class ListViewWithBoundDataTestWindow : Window
	{
		public ListViewWithBoundDataTestWindow(ILogger logger, INotifyPropertyChanged persistantData, IPlatformProvider platformProvider) 
			: base(logger, persistantData, platformProvider)
		{
			var label = new LabelView
			{
				Text = "You can select an element by clicking on it. You can select multiple elements by pressing" +
				       " Ctrl or Command while clicking."
			};
			
			var listView = new ListView
			{
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Fill,
				Orientation = OrientationOptions.Vertical,
				EntrySize = 50,
				Spacing = 5,
				ItemSource = ItemSource.From("Sausage"),
				CanMultiSelect = true
			};
			
			var elementSelected = new ListView
			{
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Fill,
				Orientation = OrientationOptions.Horizontal,
				EntrySize = 150,
				Spacing = 5,
				ItemSource = ItemSource.From("Sausage"),
			};

			Content = LayoutView.WithAdjacentVertical(new List<ILayoutable>(new ILayoutable[]{label, listView, elementSelected}));

			var context = new BoundObject();
			listView.BindingContext = context;
			listView.Bind(ItemsView.ItemSourceProperty, "Data", BindingMode.ReadOnly);
			listView.Bind(ListView.SelectedItemsProperty, "SelectedItems");

			elementSelected.BindingContext = context;
			elementSelected.Bind(ItemsView.ItemSourceProperty, "SelectedItems");
		}
	}
}