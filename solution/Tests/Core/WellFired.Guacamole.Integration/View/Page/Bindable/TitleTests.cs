using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.Page.Bindable
{
	[TestFixture]
	public class TitleTests
	{
		[SetUp]
		public void Setup()
		{
			_view = new Views.Page();
			_context = new ContextObject();
			_view.BindingContext = _context;
		}

		private Views.Page _view;
		private ContextObject _context;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextTitle()
		{
			_view.Title = "a";
			_context.Title = "b";
			Assert.That(_context.Title != _view.Title);
			_view.Bind(Views.Page.TitleProperty, nameof(_context.Title));
			Assert.That(_context.Title == _view.Title);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.Page.TitleProperty, nameof(_context.Title), BindingMode.OneWay);
			Assert.That(_context.Title == _view.Title);
			_context.Title = "a";
			Assert.That(_context.Title == _view.Title);
			_view.Title = "b";
			Assert.That(_context.Title != _view.Title);
		}
		
		[Test]
		public void ViewBaseBackgroundColorBindingDoesntWorkInTwoWayWithReadOnlyMode()
		{
			_view.Bind(Views.Page.TitleProperty, nameof(_context.Title), BindingMode.ReadOnly);
			Assert.That(_context.Title == _view.Title);
			_context.Title = "a";
			Assert.That(_context.Title == _view.Title);
			_view.Title = "b";
			Assert.That(_view.Title == "a");
			Assert.That(_context.Title == _view.Title);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInOneWay()
		{
			_view.Bind(Views.Page.TitleProperty, nameof(_context.Title));
			Assert.That(_context.Title == _view.Title);
			_context.Title = "a";
			Assert.That(_context.Title == _view.Title);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInTwoWay()
		{
			_view.Bind(Views.Page.TitleProperty, nameof(_context.Title),
				BindingMode.TwoWay);
			Assert.That(_context.Title == _view.Title);
			_context.Title = "a";
			Assert.That(_context.Title == _view.Title);
			_view.Title = "b";
			Assert.That(_context.Title == _view.Title);
		}
	}
}