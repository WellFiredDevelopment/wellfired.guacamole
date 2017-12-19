using System.Collections.Generic;
using NUnit.Framework;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Vds
{
	[TestFixture]
	public class GivenAVariableHeightViewParams
	{
		[Test]
		public void With_VariableHeight_FixedHeightData_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 0, 1, 2, 3, 4 };

			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSetWithVariableHeight(
				scrollOffset: 0, 
				visibleControlSize: 100, 
				maxEntries: 5, 
				obtainHeight: o => 10,
				visibleDataSet: ref returnedVds);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
		}
		
		[Test]
		public void With_VariableHeight_FixedHeightData_And_ScrollOffset_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 1, 2, 3, 4 };
            
			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSetWithVariableHeight(
				scrollOffset: 10, 
				visibleControlSize: 100,  
				maxEntries: 5, 
				obtainHeight: o => 10,
				visibleDataSet: ref returnedVds);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
		}
		
		[Test]
		public void With_VariableHeight_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 0, 1, 2, 3 };
            
			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSetWithVariableHeight(
				scrollOffset: 0, 
				visibleControlSize: 100,  
				maxEntries: 10, 
				obtainHeight: QuickMapping,
				visibleDataSet: ref returnedVds);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
		}
		
		[Test]
		public void With_VariableHeight_AndPartialScrollOffset_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 0, 1, 2, 3, 4 };

			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSetWithVariableHeight(
				scrollOffset: 5, 
				visibleControlSize: 100,  
				maxEntries: 10, 
				obtainHeight: QuickMapping,
				visibleDataSet: ref returnedVds);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
		}
		
		[Test]
		public void With_VariableHeight_AndWholeScrollOffset_ExpectedResultIsCorrect()
		{
			var expectedVds = new[] { 1, 2, 3, 4 };
            
			var returnedVds = new List<int>();
			VdsCalculator.CalculateVisualDataSetWithVariableHeight(
				scrollOffset: 10, 
				visibleControlSize: 100,  
				maxEntries: 10, 
				obtainHeight: QuickMapping,
				visibleDataSet: ref returnedVds);
            
			Assert.That(actual: returnedVds, expression: Is.EquivalentTo(expected: expectedVds));
		}

		private static int QuickMapping(int o)
		{
			switch (o)
			{
				case 0:
					return 10;
				case 1:
					return 20;
				case 2:
					return 30;
				case 3:
					return 40;
				case 4:
					return 50;
			}
			
			return 10;
		}
	}
}