using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Pages;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.TabbedPage
{
	[TestFixture]
	public class ConstructionOrder
	{
		[Test]
		public void When_ConstructedInOrderTemplateFirst_Then_ConstructionIsComplete()
		{
			var tabbedPage = new Pages.TabbedPage {
				ItemTemplate = DataTemplate.Of(o => new Page()),
				ItemSource = new object [] { 1, 2, 3 }
			};
			
			//Layouting the tabbed page should not influence the result of this test. This is here to prevent undesired coupling.
			ViewSizingExtensions.DoSizingAndLayout(tabbedPage, UIRect.With(1000, 500));

			Assert.That(tabbedPage.ItemSource.Count, Is.EqualTo(3));
		}
		
		[Test]
		public void When_ConstructedInOrderItemSourceFirst_Then_ConstructionIsComplete()
		{
			var tabbedPage = new Pages.TabbedPage {
				ItemSource = new object [] { 1, 2, 3 },
				ItemTemplate = DataTemplate.Of(o => new Page())
			};
			
			//Layouting the tabbed page should not influence the result of this test. This is here to prevent undesired coupling.
			ViewSizingExtensions.DoSizingAndLayout(tabbedPage, UIRect.With(1000, 500));

			Assert.That(tabbedPage.ItemSource.Count, Is.EqualTo(3));
		}
	}
}