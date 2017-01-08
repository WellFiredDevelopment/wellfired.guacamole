using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Tests.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseEnabledTests
	{
		[SetUp]
		public void Setup()
		{
			_viewBase = new Guacamole.View.ViewBase();
			_viewBaseContext = new ViewBaseContextObject();
			_viewBase.BindingContext = _viewBaseContext;
		}

		private Guacamole.View.ViewBase _viewBase;
		private ViewBaseContextObject _viewBaseContext;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextEnabled()
		{
			_viewBase.Enabled = false;
			_viewBaseContext.Enabled = !_viewBase.Enabled;
			Assert.That(_viewBaseContext.Enabled != _viewBase.Enabled);
			_viewBase.Bind(Guacamole.View.ViewBase.EnabledProperty, nameof(_viewBaseContext.Enabled));
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.EnabledProperty, nameof(_viewBaseContext.Enabled), BindingMode.OneWay);
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBase.Enabled = !_viewBase.Enabled;
			Assert.That(_viewBaseContext.Enabled != _viewBase.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInOneWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.EnabledProperty, nameof(_viewBaseContext.Enabled));
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInTwoWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.EnabledProperty, nameof(_viewBaseContext.Enabled), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBase.Enabled = !_viewBase.Enabled;
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
		}
	}
}