using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.ListView.SourceModification
{
	public class Group : ObservableCollection<LabelCellBindingContext>
	{
		public Group(string name)
		{
			CellLabelText = name;
		}

		public string CellLabelText { get; set; }
	}
	
	[TestFixture]
	public class GivenAListViewWithDefaultHeaderAndCell
	{
		private ObservableCollection<Group> _data;
		private Views.ListView _listView;
		
		private readonly LabelCellBindingContext _toAdd = new LabelCellBindingContext("Berania");
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
		public void When_EntryIsRemovedFromObservableCollection_Then_ChildrenAreModified()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 500));
			
			Assert.That(_listView.Children.Count, Is.EqualTo(10));

			_data[0].Remove(_toRemove);
			
			Assert.That(_listView.Children.Count, Is.EqualTo(9));
		}
		
		[Test]
		public void When_EntryIsAddedToObservableCollection_Then_ChildrenAreModified()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 500));
			
			Assert.That(_listView.Children.Count, Is.EqualTo(10));

			_data[1].Add(_toAdd);
			
			Assert.That(_listView.Children.Count, Is.EqualTo(11));
		}
		
		[Test]
		public void When_EntryIsSwitchedInObservableCollection_Then_ChildrenAreModified()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 500));
			
			Assert.That(_listView.Children.Count, Is.EqualTo(10));

			// Replace Amelia with Brooke
			_data[0][0] = _data[1][1];
			
			Assert.That(_listView.Children.Count, Is.EqualTo(10));
			
			Assert.That(((Views.View)_listView.Children[1]).BindingContext, Is.EqualTo(_data[1][1]));
			Assert.That(((Views.View)_listView.Children[7]).BindingContext, Is.EqualTo(_data[1][1]));
		}
		
		[Test]
		public void When_ItemSource_Is_Changed_Then_ScrollOffset_And_InitialOffset_Are_Set_To_Zero()
		{
			_listView.ScrollOffset = 50;
			
			_listView.ItemSource = new ObservableCollection<Group>();
			
			Assert.That(_listView.ScrollOffset, Is.EqualTo(0));
			Assert.That(_listView.InitialOffset, Is.EqualTo(0));
		}
	}
}