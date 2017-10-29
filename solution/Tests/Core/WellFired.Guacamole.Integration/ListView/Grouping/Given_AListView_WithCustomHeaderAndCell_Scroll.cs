using System.Collections.Generic;
using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Integration.ListView.Grouping.CustomCells;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.ListView.Grouping
{
	[TestFixture]
	public class GivenAListViewWithCustomHeaderAndCellScroll
	{
		private List<GroupHeaderBindingContext<LabelCellBindingContext>> _data;
		private Views.ListView _listView;
		private LabelCellBindingContext _scrollElement;

		[SetUp]
		public void SetUp()
		{
			_data = new List<GroupHeaderBindingContext<LabelCellBindingContext>> {
				new GroupHeaderBindingContext<LabelCellBindingContext>("A") {
					new LabelCellBindingContext("Amelia"),
					new LabelCellBindingContext("Alfie"),
					new LabelCellBindingContext("Archie")
				},
				new GroupHeaderBindingContext<LabelCellBindingContext>("B") {
					new LabelCellBindingContext("Brooke"),
					new LabelCellBindingContext("Bobby"),
					new LabelCellBindingContext("Bella"),
					new LabelCellBindingContext("Ben"),
					new LabelCellBindingContext("Bump")
				},
				new GroupHeaderBindingContext<LabelCellBindingContext>("C") {
					new LabelCellBindingContext("Calvin"),
					new LabelCellBindingContext("Calum"),
					new LabelCellBindingContext("Collin"),
					new LabelCellBindingContext("Cornelius")
				},
				new GroupHeaderBindingContext<LabelCellBindingContext>("D") {
					new LabelCellBindingContext("Darren"),
					new LabelCellBindingContext("David"),
				}
			};

			_scrollElement = _data[1][3]; // Ben
			
			_listView = new Views.ListView {
				HeaderTemplate = DataTemplate.Of(typeof(CustomHeaderTemplate)),
				ItemTemplate = DataTemplate.Of(typeof(CustomEntryTemplate)),
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Expand,
				Orientation = OrientationOptions.Vertical,
				EntrySize = 20,
				HeaderSize = 40,
				ItemSource = _data
			};
		}	
		
		[Test]
		public void With_Group_When_ScrollTo_And_Layout_Then_ChildrenAreCorrect()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 150));
			
			Assert.That(_listView.Children.Count, Is.EqualTo(6));
			
			Assert.That(_listView.Children[0], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[1], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[2], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[3], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[4], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[5], Is.TypeOf<CustomEntryTemplate>());
			
			_listView.ScrollTo(_scrollElement);
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 150));
			
			Assert.That(_listView.Children.Count, Is.EqualTo(7));
			
			Assert.That(_listView.Children[0], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[1], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[2], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[3], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[4], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[5], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[6], Is.TypeOf<CustomEntryTemplate>());
		}
		
		[Test]
		public void With_Group_When_ManualScroll_And_Layout_Then_ChildrenAreCorrect()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 150));
			
			Assert.That(_listView.Children.Count, Is.EqualTo(6));
			
			Assert.That(_listView.Children[0], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[1], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[2], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[3], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[4], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[5], Is.TypeOf<CustomEntryTemplate>());

			_listView.ScrollOffset = 20;
			
			Assert.That(_listView.Children.Count, Is.EqualTo(7));
			
			Assert.That(_listView.Children[0], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[1], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[2], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[3], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[4], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[5], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[6], Is.TypeOf<CustomEntryTemplate>());
		}
		
		[Test]
		public void With_Group_When_ManualScroll_And_Layout__And_ScrollBack_Then_ChildrenAreCorrect()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 150));
			
			Assert.That(_listView.Children.Count, Is.EqualTo(6));
			
			Assert.That(_listView.Children[0], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[1], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[2], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[3], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[4], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[5], Is.TypeOf<CustomEntryTemplate>());

			_listView.ScrollOffset = 50;
			
			Assert.That(_listView.Children.Count, Is.EqualTo(7));
			
			Assert.That(_listView.Children[0], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[1], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[2], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[3], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[4], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[5], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[6], Is.TypeOf<CustomEntryTemplate>());

			_listView.ScrollOffset = -50;
			
			Assert.That(_listView.Children.Count, Is.EqualTo(6));
			
			Assert.That(_listView.Children[0], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[1], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[2], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[3], Is.TypeOf<CustomEntryTemplate>());
			Assert.That(_listView.Children[4], Is.TypeOf<CustomHeaderTemplate>());
			Assert.That(_listView.Children[5], Is.TypeOf<CustomEntryTemplate>());
		}
	}
}