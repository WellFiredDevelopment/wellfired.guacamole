using System.Collections.Generic;
using NUnit.Framework;
using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding.Cells;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.ListView.Grouping
{
	[TestFixture]
	public class GivenAListViewWithDefaultHeaderAndCell
	{
		private List<GroupHeaderBindingContext<LabelCellBindingContext>> _data;
		private Views.ListView _listView;

		[SetUp]
		public void SetUp()
		{
			_data = new List<GroupHeaderBindingContext<LabelCellBindingContext>> 
			{
				new GroupHeaderBindingContext<LabelCellBindingContext>("A") 
				{
					new LabelCellBindingContext("Amelia"),
					new LabelCellBindingContext("Alfie"),
					new LabelCellBindingContext("Ava"),
					new LabelCellBindingContext("Archie")
				},
				new GroupHeaderBindingContext<LabelCellBindingContext>("B") 
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
		public void With_Group_When_Layout_Then_TotalContentSizeIsCorrect()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 500));
			
			Assert.That(_listView.TotalContentSize, Is.EqualTo(240));
		}
		
		[Test]
		public void With_Group_When_Layout_Then_ChildrenCountIsCorrect()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 500));
			
			Assert.That(_listView.Children.Count, Is.EqualTo(10));
		}
		
		[Test]
		public void With_Group_When_Layout_Then_ChildrenAreCorrect()
		{
			ViewSizingExtensions.DoSizingAndLayout(_listView, UIRect.With(1000, 500));
			
			Assert.That(_listView.Children[0], Is.TypeOf<HeaderCell>());
			Assert.That(_listView.Children[1], Is.TypeOf<LabelCell>());
			Assert.That(_listView.Children[2], Is.TypeOf<LabelCell>());
			Assert.That(_listView.Children[3], Is.TypeOf<LabelCell>());
			Assert.That(_listView.Children[4], Is.TypeOf<LabelCell>());
			Assert.That(_listView.Children[5], Is.TypeOf<HeaderCell>());
			Assert.That(_listView.Children[6], Is.TypeOf<LabelCell>());
			Assert.That(_listView.Children[7], Is.TypeOf<LabelCell>());
			Assert.That(_listView.Children[8], Is.TypeOf<LabelCell>());
			Assert.That(_listView.Children[9], Is.TypeOf<LabelCell>());
		}
	}
}