using System;
using System.Collections.Generic;
using NUnit.Framework;
using WellFired.Guacamole.Unit.CompositeCollection;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Vds
{
	[TestFixture]
	public class GivenANonContiguousCollection
	{
		private const float Tolerance = 0.01f;
		private Data.CompositeCollection _compositeCollection;

		[SetUp]
		public void SetUp()
		{
			var itemSource = new List<Group> {
				new Group("A") {				//0
					new GroupEntry("Amelia"),	//40
					new GroupEntry("Alfie"),	//65
					new GroupEntry("Archie")	//90
				},
				new Group("B") {				//115
					new GroupEntry("Brooke"),	//155
					new GroupEntry("Bobby"),	//180
					new GroupEntry("Bella"),	//205
					new GroupEntry("Ben"),		//230
					new GroupEntry("Bump")		//255
				},
				new Group("C") {				//280
					new GroupEntry("Calvin"),	//320
					new GroupEntry("Calum"),	//355
					new GroupEntry("Collin"),	//380
					new GroupEntry("Cornelius")	//405
				},
				new Group("D") {
					new GroupEntry("Darren"),
					new GroupEntry("David"),
					new GroupEntry("Dennis"),
				},
				new Group("E") {
					new GroupEntry("Elvis"),
					new GroupEntry("Evelyn")
				}
			};

			_compositeCollection = new Data.CompositeCollection(itemSource);
		}
		
		[Test]
		public void With_NoScrolling_ViewSizeSmallerThanTotalSize_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 0, 1, 2, 3, 4, 5, 6 };

			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSet(
				scrollOffset: 0,
				visibleControlSize: 200, 
				collection: _compositeCollection,
				headerSize: 40, 
				entrySize: 25,
				visibleDataSet: ref returnedVds,
				initialOffset: out var initialOffset);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
			Assert.That(initialOffset, Is.EqualTo(0));
		}
		
		[Test]
		public void With_HeaderPartiallyVisible_ViewSizeSmallerThanTotalSize_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 4, 5, 6, 7, 8, 9, 10, 11 };
            
			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSet(
				scrollOffset: 125,
				visibleControlSize: 200, 
				collection: _compositeCollection,
				headerSize: 40, 
				entrySize: 25,
				visibleDataSet: ref returnedVds,
				initialOffset: out var initialOffset);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
			Assert.That(Math.Abs(initialOffset - (-10)), Is.LessThan(Tolerance));
		}
		
		[Test]
		public void With_GroupPartiallyVisible_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 7, 8, 9, 10, 11, 12, 13, 14 };
            
			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSet(
				scrollOffset: 210,
				visibleControlSize: 200, 
				collection: _compositeCollection,
				headerSize: 40, 
				entrySize: 25,
				visibleDataSet: ref returnedVds,
				initialOffset: out var initialOffset);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
			Assert.That(Math.Abs(initialOffset - (-5)), Is.LessThan(Tolerance));
		}
	}
}