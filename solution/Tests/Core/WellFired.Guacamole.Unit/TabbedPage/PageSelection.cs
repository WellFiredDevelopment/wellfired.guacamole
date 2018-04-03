using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Pages;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.TabbedPage
{
	[TestFixture]
	public class PageSelection
	{
		[Test]
		public void When_ItemSource_Is_Null_Then_SelectedPage_Reference_Is_Released()
		{
			var tabbedPage = new Pages.TabbedPage {
				ItemTemplate = DataTemplate.Of(o => new Page()),
				ItemSource = new object [] { 1, 2, 3 }
			};
			
			//Layouting the tabbed page should not influence the result of this test. This is here to prevent undesired coupling.
			ViewSizingExtensions.DoSizingAndLayout(tabbedPage, UIRect.With(1000, 500));
			
			Assert.That(tabbedPage.SelectedPage, Is.SameAs(tabbedPage.ItemSource[0]));
			
			tabbedPage.ItemSource = null;
			ViewSizingExtensions.DoSizingAndLayout(tabbedPage, UIRect.With(1000, 500));
			Assert.That(tabbedPage.SelectedPage, Is.Null);
		}
		
		[Test]
		public void When_Page_Is_Selected_Before_Source_Is_Specifid_Then_Right_Page_Is_A_Posteriori_Displayed()
		{
			object selectedPage = 1;

			var itemSource = new[] {1, selectedPage, 3};
			
			var tabbedPage = new Pages.TabbedPage {
				ItemTemplate = DataTemplate.Of(o => new Page()),
				SelectedPage = selectedPage
			};
			
			//Layouting the tabbed page should not influence the result of this test. This is here to prevent undesired coupling.
			ViewSizingExtensions.DoSizingAndLayout(tabbedPage, UIRect.With(1000, 500));

			tabbedPage.ItemSource = itemSource;
			
			Assert.That(tabbedPage.SelectedPage, Is.SameAs(selectedPage));
		}
	}
}