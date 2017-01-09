using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Tests.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseControlStateTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextControlState()
		{
			_view.ControlState = ControlState.Active;
			_viewBaseContext.ControlState = ControlState.Disabled;
			Assert.That(_viewBaseContext.ControlState != _view.ControlState);
			_view.Bind(Views.View.ControlStateProperty, nameof(_viewBaseContext.ControlState));
			Assert.That(_viewBaseContext.ControlState == _view.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.ControlStateProperty, nameof(_viewBaseContext.ControlState),
				BindingMode.OneWay);
			Assert.That(_viewBaseContext.ControlState == _view.ControlState);
			_viewBaseContext.ControlState = ControlState.Disabled;
			Assert.That(_viewBaseContext.ControlState == _view.ControlState);
			_view.ControlState = ControlState.Hover;
			Assert.That(_viewBaseContext.ControlState != _view.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingWorksInOneWay()
		{
			_view.Bind(Views.View.ControlStateProperty, nameof(_viewBaseContext.ControlState));
			Assert.That(_viewBaseContext.ControlState == _view.ControlState);
			_viewBaseContext.ControlState = ControlState.Disabled;
			Assert.That(_viewBaseContext.ControlState == _view.ControlState);
		}

		[Test]
		public void ViewBaseControlStateBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.ControlStateProperty, nameof(_viewBaseContext.ControlState),
				BindingMode.TwoWay);
			Assert.That(_viewBaseContext.ControlState == _view.ControlState);
			_viewBaseContext.ControlState = ControlState.Disabled;
			Assert.That(_viewBaseContext.ControlState == _view.ControlState);
			_view.ControlState = ControlState.Hover;
			Assert.That(_viewBaseContext.ControlState == _view.ControlState);
		}
	}
}