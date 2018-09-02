using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Integration.View.View.Bindable;

namespace WellFired.Guacamole.Integration.View.ViewContent
{
	[TestFixture]
	public class ContentEnabledTests
	{
		[SetUp]
		public void Setup()
		{
			_view = new Views.View();
			_viewContent = new Views.View();
			_view.Content = _viewContent;
			_context = new ContextObject();
			_view.BindingContext = _context;
		}

		private Views.View _view;
		private Views.View _viewContent;
		private ContextObject _context;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextEnabled()
		{
			_viewContent.Enabled = false;
			_context.Enabled = !_viewContent.Enabled;
			Assert.That(_context.Enabled != _viewContent.Enabled);
			_viewContent.Bind(Views.View.EnabledProperty, nameof(_context.Enabled));
			Assert.That(_context.Enabled == _viewContent.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewContent.Bind(Views.View.EnabledProperty, nameof(_context.Enabled), BindingMode.OneWay);
			Assert.That(_context.Enabled == _viewContent.Enabled);
			_context.Enabled = !_context.Enabled;
			Assert.That(_context.Enabled == _viewContent.Enabled);
			_viewContent.Enabled = !_viewContent.Enabled;
			Assert.That(_context.Enabled != _viewContent.Enabled);
		}
		
		[Test]
		public void ViewBaseEnabledBindingDoesntWorkInTwoWayWithReadOnlyMode()
		{
			_viewContent.Bind(Views.View.EnabledProperty, nameof(_context.Enabled), BindingMode.ReadOnly);
			Assert.That(_context.Enabled == _viewContent.Enabled);
			
			_context.Enabled = !_context.Enabled;
			Assert.That(_context.Enabled == _viewContent.Enabled);

			_viewContent.Enabled = !_context.Enabled;
			Assert.That(_viewContent.Enabled == _context.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInOneWay()
		{
			_viewContent.Bind(Views.View.EnabledProperty, nameof(_context.Enabled));
			Assert.That(_context.Enabled == _viewContent.Enabled);
			_context.Enabled = !_context.Enabled;
			Assert.That(_context.Enabled == _viewContent.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInTwoWay()
		{
			_viewContent.Bind(Views.View.EnabledProperty, nameof(_context.Enabled), BindingMode.TwoWay);
			Assert.That(_context.Enabled == _viewContent.Enabled);
			_context.Enabled = !_context.Enabled;
			Assert.That(_context.Enabled == _viewContent.Enabled);
			_viewContent.Enabled = !_viewContent.Enabled;
			Assert.That(_context.Enabled == _viewContent.Enabled);
		}
	}
}