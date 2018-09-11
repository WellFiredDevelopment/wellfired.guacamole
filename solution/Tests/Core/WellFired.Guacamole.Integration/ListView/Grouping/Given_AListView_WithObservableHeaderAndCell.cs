using NUnit.Framework;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.ListView.Grouping
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
	
	[TestFixture]
	public class GivenAListViewWithObservableHeaderAndCell
	{
		private ObservableCollection<Group> _data;
		private Views.ListView _listView;
		private LabelCellBindingContext _toRemove;
		
		[SetUp]
		public void SetUp()
		{	
			_toRemove = new LabelCellBindingContext("Ava");
			_data = new ObservableCollection<Group> 
			{
				new Group("A") 
				{
					new LabelCellBindingContext("Amelia"),
					new LabelCellBindingContext("Alfie"),
					_toRemove,
					new LabelCellBindingContext("Archie")
				},
				new Group("B")
				{
					new LabelCellBindingContext("Brooke"),
					new LabelCellBindingContext("Bobby"),
					new LabelCellBindingContext("Bella"),
					new LabelCellBindingContext("Ben")
				}
			}; 
			
			_listView = new Views.ListView {
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Expand,
				Orientation = OrientationOptions.Horizontal,
				EntrySize = 20,
				HeaderSize = 40,
				ItemSource = _data
			};
		}
		
		[Test]
		public void When_AddItem_Then_ScrollOffset_And_TotalContentSize_Are_Correct()
		{
			//We display the list in an available space of 140, and set up scroll offset to 100
			//in order to display only items from Archie to Ben

			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(140, 500));
			_listView.ScrollOffset = 100;
			
			Assert.That(((LabelCell)_listView.Children[0]).Text, Is.EqualTo("Archie"));
			Assert.That(((HeaderCell)_listView.Children[1]).Text, Is.EqualTo("B"));
			Assert.That(((LabelCell)_listView.Children[2]).Text, Is.EqualTo("Brooke"));
			Assert.That(((LabelCell)_listView.Children[3]).Text, Is.EqualTo("Bobby"));
			Assert.That(((LabelCell)_listView.Children[4]).Text, Is.EqualTo("Bella"));
			Assert.That(((LabelCell)_listView.Children[5]).Text, Is.EqualTo("Ben"));
			
			//We add an item on top of the displayed elements
			_data[0].Insert(0, new LabelCellBindingContext("Adele"));

			Assert.That(_listView.TotalContentSize, Is.EqualTo(260));
			Assert.That(_listView.ScrollOffset, Is.EqualTo(100));
		}
		
		[Test]
		public void When_AddItem_In_The_Middle_Then_ScrollOffset_And_TotalContentSize_Are_Correct()
		{
			//We display the list in an available space of 140, and set up scroll offset to 100
			//in order to display only items from Archie to Ben

			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(140, 500));
			_listView.ScrollOffset = 100;
			
			Assert.That(((LabelCell)_listView.Children[0]).Text, Is.EqualTo("Archie"));
			Assert.That(((HeaderCell)_listView.Children[1]).Text, Is.EqualTo("B"));
			Assert.That(((LabelCell)_listView.Children[2]).Text, Is.EqualTo("Brooke"));
			Assert.That(((LabelCell)_listView.Children[3]).Text, Is.EqualTo("Bobby"));
			Assert.That(((LabelCell)_listView.Children[4]).Text, Is.EqualTo("Bella"));
			Assert.That(((LabelCell)_listView.Children[5]).Text, Is.EqualTo("Ben"));
			
			//We add an item in the middle of the displayed elements
			_data[0].Insert(1, new LabelCellBindingContext("Amelie"));
			_data[0].Insert(3, new LabelCellBindingContext("Arakiri"));

			Assert.That(_listView.TotalContentSize, Is.EqualTo(280));
			Assert.That(_listView.ScrollOffset, Is.EqualTo(100));
			
			Assert.That(((LabelCell)_listView.Children[0]).Text, Is.EqualTo("Arakiri"));
			Assert.That(((LabelCell)_listView.Children[1]).Text, Is.EqualTo("Ava"));
			Assert.That(((LabelCell)_listView.Children[2]).Text, Is.EqualTo("Archie"));
			Assert.That(((HeaderCell)_listView.Children[3]).Text, Is.EqualTo("B"));
			Assert.That(((LabelCell)_listView.Children[4]).Text, Is.EqualTo("Brooke"));
			Assert.That(((LabelCell)_listView.Children[5]).Text, Is.EqualTo("Bobby"));
		}
		
		[Test]
		public void With_Group_When_AddItem_Then_ScrollOffset_And_TotalContentSize_Are_Correct()
		{
			//We display the list in an available space of 140, and set up scrol to 100
			//in order to display only items from Archie to Ben

			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(140, 500));
			_listView.ScrollOffset = 100;
			
			Assert.That(((LabelCell)_listView.Children[0]).Text, Is.EqualTo("Archie"));
			Assert.That(((HeaderCell)_listView.Children[1]).Text, Is.EqualTo("B"));
			Assert.That(((LabelCell)_listView.Children[2]).Text, Is.EqualTo("Brooke"));
			Assert.That(((LabelCell)_listView.Children[3]).Text, Is.EqualTo("Bobby"));
			Assert.That(((LabelCell)_listView.Children[4]).Text, Is.EqualTo("Bella"));
			Assert.That(((LabelCell)_listView.Children[5]).Text, Is.EqualTo("Ben"));

			//We add an item on top of the displayed elements
			_data.Insert(0, new Group("A-Bis") {
				new LabelCellBindingContext("Amelia"),
				new LabelCellBindingContext("Alfie"),
				new LabelCellBindingContext("Archie")
			});

			Assert.That(_listView.TotalContentSize, Is.EqualTo(340));
			
			//The scrolloffset does not change. We could also modify scrolloffset to ensure that the list item at the top of the
			//visible space stays at the top, but this imply more calculation and is not necessary for the moment.
			Assert.That(_listView.ScrollOffset, Is.EqualTo(100));
		}
		
		[Test]
		public void When_RemoveItem_Then_ScrollOffset_And_TotalContentSize_Are_Correct()
		{
			//We display the list in an available space of 140, and set up scrol to 100
			//in order to display only items from Archie to Ben

			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(140, 500));
			_listView.ScrollOffset = 100;
			
			Assert.That(((LabelCell)_listView.Children[0]).Text, Is.EqualTo("Archie"));
			Assert.That(((HeaderCell)_listView.Children[1]).Text, Is.EqualTo("B"));
			Assert.That(((LabelCell)_listView.Children[2]).Text, Is.EqualTo("Brooke"));
			Assert.That(((LabelCell)_listView.Children[3]).Text, Is.EqualTo("Bobby"));
			Assert.That(((LabelCell)_listView.Children[4]).Text, Is.EqualTo("Bella"));
			Assert.That(((LabelCell)_listView.Children[5]).Text, Is.EqualTo("Ben"));

			//We remove an item on top of the displayed elements
			_data[0].Remove(_toRemove);

			Assert.That(_listView.TotalContentSize, Is.EqualTo(220));
			Assert.That(_listView.ScrollOffset, Is.EqualTo(80));
		}
		
		[Test]
		public void With_Group_When_RemoveItem_Then_ScrollOffset_And_TotalContentSize_Are_Correct()
		{
			//We display the list in an available space of 140, and set up scrol to 100
			//in order to display only items from Archie to Ben

			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(140, 500));
			_listView.ScrollOffset = 100;
			
			Assert.That(((LabelCell)_listView.Children[0]).Text, Is.EqualTo("Archie"));
			Assert.That(((HeaderCell)_listView.Children[1]).Text, Is.EqualTo("B"));
			Assert.That(((LabelCell)_listView.Children[2]).Text, Is.EqualTo("Brooke"));
			Assert.That(((LabelCell)_listView.Children[3]).Text, Is.EqualTo("Bobby"));
			Assert.That(((LabelCell)_listView.Children[4]).Text, Is.EqualTo("Bella"));
			Assert.That(((LabelCell)_listView.Children[5]).Text, Is.EqualTo("Ben"));

			//We remove an item on top of the list
			_data.RemoveAt(0);

			Assert.That(_listView.TotalContentSize, Is.EqualTo(120));
			Assert.That(_listView.ScrollOffset, Is.EqualTo(0));
		}
	}
}