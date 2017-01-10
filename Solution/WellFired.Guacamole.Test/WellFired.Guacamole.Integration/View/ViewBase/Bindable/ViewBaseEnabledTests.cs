using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseEnabledTests
	{
		[SetUp]
		public void Setup()
		{
			_view = new Views.View();
			_viewBaseContext = new ViewBaseContextObject();
			_view.BindingContext = _viewBaseContext;
		}

		private Views.View _view;
		private ViewBaseContextObject _viewBaseContext;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextEnabled()
		{
			_view.Enabled = false;
			_viewBaseContext.Enabled = !_view.Enabled;
			Assert.That(_viewBaseContext.Enabled != _view.Enabled);
			_view.Bind(Views.View.EnabledProperty, nameof(_viewBaseContext.Enabled));
			Assert.That(_viewBaseContext.Enabled == _view.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.EnabledProperty, nameof(_viewBaseContext.Enabled), BindingMode.OneWay);
			Assert.That(_viewBaseContext.Enabled == _view.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _view.Enabled);
			_view.Enabled = !_view.Enabled;
			Assert.That(_viewBaseContext.Enabled != _view.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInOneWay()
		{
			_view.Bind(Views.View.EnabledProperty, nameof(_viewBaseContext.Enabled));
			Assert.That(_viewBaseContext.Enabled == _view.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _view.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.EnabledProperty, nameof(_viewBaseContext.Enabled), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.Enabled == _view.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _view.Enabled);
			_view.Enabled = !_view.Enabled;
			Assert.That(_viewBaseContext.Enabled == _view.Enabled);
		}
	}
}