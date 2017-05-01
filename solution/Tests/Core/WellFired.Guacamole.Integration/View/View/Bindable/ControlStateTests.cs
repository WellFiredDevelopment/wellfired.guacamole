using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class ControlStateTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextControlState()
		{
			_view.ControlState = ControlState.Active;
			_context.ControlState = ControlState.Disabled;
			Assert.That(_context.ControlState != _view.ControlState);
			_view.Bind(Views.View.ControlStateProperty, nameof(_context.ControlState));
			Assert.That(_context.ControlState == _view.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.ControlStateProperty, nameof(_context.ControlState),
				BindingMode.OneWay);
			Assert.That(_context.ControlState == _view.ControlState);
			_context.ControlState = ControlState.Disabled;
			Assert.That(_context.ControlState == _view.ControlState);
			_view.ControlState = ControlState.Hover;
			Assert.That(_context.ControlState != _view.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingWorksInOneWay()
		{
			_view.Bind(Views.View.ControlStateProperty, nameof(_context.ControlState));
			Assert.That(_context.ControlState == _view.ControlState);
			_context.ControlState = ControlState.Disabled;
			Assert.That(_context.ControlState == _view.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.ControlStateProperty, nameof(_context.ControlState),
				BindingMode.TwoWay);
			Assert.That(_context.ControlState == _view.ControlState);
			_context.ControlState = ControlState.Disabled;
			Assert.That(_context.ControlState == _view.ControlState);
			_view.ControlState = ControlState.Hover;
			Assert.That(_context.ControlState == _view.ControlState);
		}
	}
}