using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Test.Acceptance.UI.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseEnabledTests
	{
		private View.ViewBase _viewBase;
		private ViewBaseContextObject _viewBaseContext;

		[SetUp]
		public void OneTimeSetup()
		{
			_viewBase = new View.ViewBase();
			_viewBaseContext = new ViewBaseContextObject();
			_viewBase.BindingContext = _viewBaseContext;
		}

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextEnabled()
		{
			_viewBase.Enabled = false;
			_viewBaseContext.Enabled = !_viewBase.Enabled;
			Assert.That(_viewBaseContext.Enabled != _viewBase.Enabled);
			_viewBase.Bind(View.ViewBase.EnabledProperty, nameof(_viewBaseContext.Enabled));
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInOneWay()
		{
			_viewBase.Bind(View.ViewBase.EnabledProperty, nameof(_viewBaseContext.Enabled));
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingWorksInTwoWay()
		{
			_viewBase.Bind(View.ViewBase.EnabledProperty, nameof(_viewBaseContext.Enabled), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBase.Enabled = !_viewBase.Enabled;
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
		}

		[Test]
		public void ViewBaseEnabledBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(View.ViewBase.EnabledProperty, nameof(_viewBaseContext.Enabled), BindingMode.OneWay);
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBaseContext.Enabled = !_viewBaseContext.Enabled;
			Assert.That(_viewBaseContext.Enabled == _viewBase.Enabled);
			_viewBase.Enabled = !_viewBase.Enabled;
			Assert.That(_viewBaseContext.Enabled != _viewBase.Enabled);
		}
	}
}