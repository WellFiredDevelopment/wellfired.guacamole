using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Test.Acceptance.UI.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseControlStateTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextControlState()
		{
			_viewBase.ControlState = ControlState.Active;
			_viewBaseContext.ControlState = ControlState.Disabled;
			Assert.That(_viewBaseContext.ControlState != _viewBase.ControlState);
			_viewBase.Bind(View.ViewBase.ControlStateProperty, nameof(_viewBaseContext.ControlState));
			Assert.That(_viewBaseContext.ControlState == _viewBase.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingWorksInOneWay()
		{
			_viewBase.Bind(View.ViewBase.ControlStateProperty, nameof(_viewBaseContext.ControlState));
			Assert.That(_viewBaseContext.ControlState == _viewBase.ControlState);
			_viewBaseContext.ControlState = ControlState.Disabled;
			Assert.That(_viewBaseContext.ControlState == _viewBase.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingWorksInTwoWay()
		{
			_viewBase.Bind(View.ViewBase.ControlStateProperty, nameof(_viewBaseContext.ControlState), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.ControlState == _viewBase.ControlState);
			_viewBaseContext.ControlState = ControlState.Disabled;
			Assert.That(_viewBaseContext.ControlState == _viewBase.ControlState);
			_viewBase.ControlState = ControlState.Hover;
			Assert.That(_viewBaseContext.ControlState == _viewBase.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(View.ViewBase.ControlStateProperty, nameof(_viewBaseContext.ControlState), BindingMode.OneWay);
			Assert.That(_viewBaseContext.ControlState == _viewBase.ControlState);
			_viewBaseContext.ControlState = ControlState.Disabled;
			Assert.That(_viewBaseContext.ControlState == _viewBase.ControlState);
			_viewBase.ControlState = ControlState.Hover;
			Assert.That(_viewBaseContext.ControlState != _viewBase.ControlState);
		}
	}
}