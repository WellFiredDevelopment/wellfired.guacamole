using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class BackgroundColorTests
	{
		[SetUp]
		public void Setup()
		{
			_view = new Views.View();
			_context = new ContextObject();
			_view.BindingContext = _context;
		}

		private Views.View _view;
		private ContextObject _context;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextBackgroundColor()
		{
			_view.BackgroundColor = UIColor.Blue;
			_context.BackgroundColor = UIColor.Red;
			Assert.That(_context.BackgroundColor != _view.BackgroundColor);
			_view.Bind(Views.View.BackgroundColorProperty, nameof(_context.BackgroundColor));
			Assert.That(_context.BackgroundColor == _view.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.BackgroundColorProperty, nameof(_context.BackgroundColor));
			Assert.That(_context.BackgroundColor == _view.BackgroundColor);
			_context.BackgroundColor = UIColor.Blue;
			Assert.That(_context.BackgroundColor == _view.BackgroundColor);
			_view.BackgroundColor = UIColor.Red;
			Assert.That(_context.BackgroundColor != _view.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInOneWay()
		{
			_view.Bind(Views.View.BackgroundColorProperty, nameof(_context.BackgroundColor));
			Assert.That(_context.BackgroundColor == _view.BackgroundColor);
			_context.BackgroundColor = UIColor.Brown;
			Assert.That(_context.BackgroundColor == _view.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.BackgroundColorProperty, nameof(_context.BackgroundColor),
				BindingMode.TwoWay);
			Assert.That(_context.BackgroundColor == _view.BackgroundColor);
			_context.BackgroundColor = UIColor.Blue;
			Assert.That(_context.BackgroundColor == _view.BackgroundColor);
			_view.BackgroundColor = UIColor.Red;
			Assert.That(_context.BackgroundColor == _view.BackgroundColor);
		}
	}
}