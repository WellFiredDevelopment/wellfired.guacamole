using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class EnabledTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextEnabled()
		{
			_view.Enabled = false;
			_context.Enabled = !_view.Enabled;
			Assert.That(_context.Enabled != _view.Enabled);
			_view.Bind(Views.View.EnabledProperty, nameof(_context.Enabled));
			Assert.That(_context.Enabled == _view.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.EnabledProperty, nameof(_context.Enabled), BindingMode.OneWay);
			Assert.That(_context.Enabled == _view.Enabled);
			_context.Enabled = !_context.Enabled;
			Assert.That(_context.Enabled == _view.Enabled);
			_view.Enabled = !_view.Enabled;
			Assert.That(_context.Enabled != _view.Enabled);
		}
		
		[Test]
		public void ViewBaseEnabledBindingDoesntWorkInTwoWayWithReadOnlyMode()
		{
			_view.Bind(Views.View.EnabledProperty, nameof(_context.Enabled), BindingMode.ReadOnly);
			Assert.That(_context.Enabled == _view.Enabled);
			
			_context.Enabled = !_context.Enabled;
			Assert.That(_context.Enabled == _view.Enabled);

			_view.Enabled = !_context.Enabled;
			Assert.That(_view.Enabled == _context.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInOneWay()
		{
			_view.Bind(Views.View.EnabledProperty, nameof(_context.Enabled));
			Assert.That(_context.Enabled == _view.Enabled);
			_context.Enabled = !_context.Enabled;
			Assert.That(_context.Enabled == _view.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.EnabledProperty, nameof(_context.Enabled), BindingMode.TwoWay);
			Assert.That(_context.Enabled == _view.Enabled);
			_context.Enabled = !_context.Enabled;
			Assert.That(_context.Enabled == _view.Enabled);
			_view.Enabled = !_view.Enabled;
			Assert.That(_context.Enabled == _view.Enabled);
		}
	}
}