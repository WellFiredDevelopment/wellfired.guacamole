using System.Collections.Generic;
using NUnit.Framework;

namespace WellFired.Guacamole.Unit.ListView
{
	[TestFixture]
	public class GivenListViewVdsChangeListener
	{
		[Test]
		public void When_Item_Removed_From_Front_Of_VDS_Then_List_Updated_Correctly()
		{
			var listView = GetTestList(entrySize:25, spacing:3);
			
			listView.ItemLeftVds(0, true);
			Assert.That(listView.InitialOffset, Is.EqualTo(25 + 3));
		}
		
		[Test]
		public void When_Item_Removed_From_Bottom_Of_VDS_Then_List_Updated_Correctly()
		{
			var listView = GetTestList(entrySize:25, spacing:3);
			
			listView.ItemLeftVds(3, false);
			Assert.That(listView.InitialOffset, Is.EqualTo(0));
		}
		
		[Test]
		public void When_Item_Added_At_Front_Of_VDS_Then_List_Updated_Correctly()
		{
			var listView = GetTestList(entrySize:25, spacing:3);
			
			listView.ItemEnteredVds(2, true);
			Assert.That(listView.InitialOffset, Is.EqualTo(-25 - 3));
		}
		
		[Test]
		public void When_Item_Added_At_Bottom_Of_VDS_Then_List_Updated_Correctly()
		{
			var listView = GetTestList(entrySize:25, spacing:3);
			
			listView.ItemEnteredVds(3, false);
			Assert.That(listView.InitialOffset, Is.EqualTo(0));
		}

		private static Views.ListView GetTestList(int entrySize, int spacing)
		{
			return new Views.ListView {
				EntrySize = entrySize,
				Spacing = spacing,
				ItemSource = new List<string>(new[] {"1", "2", "3", "4", "5"})
			};
		}
	}
}