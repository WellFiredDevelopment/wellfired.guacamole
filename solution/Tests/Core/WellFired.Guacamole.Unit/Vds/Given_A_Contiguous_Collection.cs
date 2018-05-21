using System;
using System.Collections.Generic;
using NUnit.Framework;
using WellFired.Guacamole.Data.Collection;
using WellFired.Guacamole.Unit.CompositeCollection;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Vds
{
	[TestFixture]
	public class GivenAContiguousCollection
	{
		private const float Tolerance = 0.01f;
		private Data.CompositeCollection _compositeCollection;

		[SetUp]
		public void SetUp()
		{
			var itemSource = new ObservableCollection<GroupEntry> {
				new GroupEntry("Amelia"),
				new GroupEntry("Alfie"),
				new GroupEntry("Archie"),
				new GroupEntry("Brooke"),
				new GroupEntry("Bobby"),
				new GroupEntry("Bella"),
				new GroupEntry("Ben"),
				new GroupEntry("Bump"),
				new GroupEntry("Calvin"),
				new GroupEntry("Calum")
			};

			_compositeCollection = new Data.CompositeCollection(itemSource);
		}
		
		[Test]
		public void With_NoScrolling_ViewSizeSmallerThanTotalSize_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 0, 1, 2, 3 };

			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSet(
				scrollOffset: 0,
				visibleControlSize: 100, 
				collection: _compositeCollection,
				headerSize: 5, 
				entrySize: 25,
				visibleDataSet: ref returnedVds,
				initialOffset: out var initialOffset);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
			Assert.That(initialOffset, Is.EqualTo(0));
		}
		
		[Test]
		public void With_Scrolling_ViewSizeSmallerThanTotalSize_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 1, 2, 3, 4 };
            
			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSet(
				scrollOffset: 29,
				visibleControlSize: 100, 
				collection: _compositeCollection,
				headerSize: 5, 
				entrySize: 25,
				visibleDataSet: ref returnedVds,
				initialOffset: out var initialOffset);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
			Assert.That(Math.Abs(initialOffset - (-4)), Is.LessThan(Tolerance));
		}
		
		[Test]
		public void With_NoScrolling_ViewSizeBiggerThanTotalSize_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            
			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSet(
				scrollOffset: 0,
				visibleControlSize: 1000, 
				collection: _compositeCollection,
				headerSize: 5, 
				entrySize: 25,
				visibleDataSet: ref returnedVds,
				initialOffset: out var initialOffset);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
			Assert.That(initialOffset, Is.EqualTo(0));
		}
	}
}