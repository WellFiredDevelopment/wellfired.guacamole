using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.ListView.SelectedObjectModification
{
	[TestFixture]
	public class GivenAListViewWithSelectedItem
	{
		public class Group : ObservableCollection<LabelCellBindingContext>, IDefaultCellContext
		{
			public Group(string name)
			{
				CellLabelText = name;
			}

			public string CellLabelText { get; set; }
			public bool IsSelected { get; set; }
		}
		
		[Test]
		public void When_SelectedItemIsSetToNull_Then_NoLongerSelected()
		{
			var data = new ObservableCollection<Group> 
			{
				new Group("A") 
				{
					new LabelCellBindingContext("Amelia"),
					new LabelCellBindingContext("Alfie")
				},
				new Group("B") 
				{
					new LabelCellBindingContext("Brooke"),
					new LabelCellBindingContext("Bobby")
				}
			}; 
			
			var listView = new Views.ListView {
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Expand,
				Orientation = OrientationOptions.Horizontal,
				EntrySize = 20,
				HeaderSize = 40,
				ItemSource = data
			};
			
			ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(1000, 1000));
			
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(0));
			
			listView.SelectedItem = data[1][1];
			
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(1));
			
			listView.SelectedItem = null;
			
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(0));
			
			listView.SelectedItems.Add(data[0][0]);
			listView.SelectedItems.Add(data[0][1]);
			
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(2));
			
			listView.SelectedItems.Remove(data[0][1]);
			
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(1));
		}
		
		[Test]
		public void When_BoundToAnObjectAndSelectedItemIsSetToNull_Then_NoLongerSelected()
		{
			var data = new ObservableCollection<Group> 
			{
				new Group("A") 
				{
					new LabelCellBindingContext("Amelia"),
					new LabelCellBindingContext("Alfie")
				},
				new Group("B") 
				{
					new LabelCellBindingContext("Brooke"),
					new LabelCellBindingContext("Bobby")
				}
			}; 
			
			var listView = new Views.ListView {
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Expand,
				Orientation = OrientationOptions.Horizontal,
				EntrySize = 20,
				HeaderSize = 40,
				ItemSource = data
			};
			
			ViewSizingExtensions.DoSizingAndLayout(listView, UIRect.With(1000, 1000));

			var boundObject = new ListBoundObject();
			listView.Bind(Views.ListView.SelectedItemProperty, "SelectedItem", BindingMode.TwoWay);
			listView.Bind(Views.ListView.SelectedItemsProperty, "SelectedItems", BindingMode.TwoWay);
			listView.BindingContext = boundObject;
			
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(0));
			
			boundObject.SelectedItem = data[1][1];
			
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(1));
			Assert.That(boundObject.SelectedItems.Count, Is.EqualTo(1));
			Assert.That(boundObject.SelectedItems[0], Is.EqualTo(boundObject.SelectedItem));
			
			boundObject.SelectedItem = null;
			
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(0));
			
			boundObject.SelectedItems.Add(data[0][0]);
			
			Assert.That(listView.Children.Cast<LabelCell>().Where(o => o.IsSelected).Select(o => o.Text), 
				Is.EquivalentTo(new []{"Amelia"}));
			
			boundObject.SelectedItems.Add(data[1][0]);
			boundObject.SelectedItems.Add(data[1][1]);
			
			Assert.That(listView.Children.Cast<LabelCell>().Where(o => o.IsSelected).Select(o => o.Text), 
				Is.EquivalentTo(new []{"Amelia", "Brooke", "Bobby"}));

			boundObject.SelectedItems.Remove(data[1][1]);
			
			Assert.That(listView.Children.Cast<LabelCell>().Where(o => o.IsSelected).Select(o => o.Text), 
				Is.EquivalentTo(new []{"Amelia", "Brooke"}));

			boundObject.SelectedItems = null;
			Assert.That(listView.Children.Cast<Cell>().Count(o => o.IsSelected), Is.EqualTo(0));
		}
	}

	public class ListBoundObject : ObservableBase, IDefaultCellContext
	{
		private object _selectedItem;
		private ObservableCollection<INotifyPropertyChanged> _selectedItems;
		private bool _isSelected;
		private string _cellLabelText;

		public object SelectedItem
		{
			get => _selectedItem;
			set => SetProperty(ref _selectedItem, value);
		}

		public ObservableCollection<INotifyPropertyChanged> SelectedItems
		{
			get => _selectedItems;
			set => SetProperty(ref _selectedItems, value);
		}

		public bool IsSelected { 
			get => _isSelected;
			set => SetProperty(ref _isSelected, value);
		}
		
		public string CellLabelText { 
			get => _cellLabelText;
			set => SetProperty(ref _cellLabelText, value); 
		}
	}
}