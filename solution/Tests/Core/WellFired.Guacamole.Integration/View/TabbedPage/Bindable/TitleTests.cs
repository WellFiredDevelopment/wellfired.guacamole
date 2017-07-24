using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.TabbedPage.Bindable
{
	[TestFixture]
	public class TitleTests
	{
		[SetUp]
		public void Setup()
		{
			_view = new Views.TabbedPage();
			_context = new ContextObject();
			_view.BindingContext = _context;
		}

		private Views.TabbedPage _view;
		private ContextObject _context;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextTitle()
		{
			_view.SelectedPage = "a";
			_context.SelectedPage = "b";
			Assert.That(_context.SelectedPage != (string) _view.SelectedPage);
			_view.Bind(Views.TabbedPage.SelectedPageProperty, nameof(_context.SelectedPage));
			Assert.That(_context.SelectedPage == (string) _view.SelectedPage);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.TabbedPage.SelectedPageProperty, nameof(_context.SelectedPage));
			Assert.That(_context.SelectedPage == (string) _view.SelectedPage);
			_context.SelectedPage = "a";
			Assert.That(_context.SelectedPage == (string) _view.SelectedPage);
			_view.SelectedPage = "b";
			Assert.That(_context.SelectedPage != (string) _view.SelectedPage);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInOneWay()
		{
			_view.Bind(Views.TabbedPage.SelectedPageProperty, nameof(_context.SelectedPage));
			Assert.That(_context.SelectedPage == (string) _view.SelectedPage);
			_context.SelectedPage = "a";
			Assert.That(_context.SelectedPage == (string) _view.SelectedPage);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInTwoWay()
		{
			_view.Bind(Views.TabbedPage.SelectedPageProperty, nameof(_context.SelectedPage),
				BindingMode.TwoWay);
			Assert.That(_context.SelectedPage == (string) _view.SelectedPage);
			_context.SelectedPage = "a";
			Assert.That(_context.SelectedPage == (string) _view.SelectedPage);
			_view.SelectedPage = "b";
			Assert.That(_context.SelectedPage == (string) _view.SelectedPage);
		}
	}
}