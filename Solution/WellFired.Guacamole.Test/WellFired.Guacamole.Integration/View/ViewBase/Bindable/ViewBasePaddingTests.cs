using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBasePaddingTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextPadding()
		{
			_view.Padding = new UIPadding(0);
			_viewBaseContext.Padding = new UIPadding(1);
			Assert.That(_viewBaseContext.Padding != _view.Padding);
			_view.Bind(Views.View.PaddingProperty, nameof(_viewBaseContext.Padding));
			Assert.That(_viewBaseContext.Padding == _view.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.PaddingProperty, nameof(_viewBaseContext.Padding));
			Assert.That(_viewBaseContext.Padding == _view.Padding);
			_viewBaseContext.Padding = new UIPadding(0);
			Assert.That(_viewBaseContext.Padding == _view.Padding);
			_view.Padding = new UIPadding(1);
			Assert.That(_viewBaseContext.Padding != _view.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingWorksInOneWay()
		{
			_view.Bind(Views.View.PaddingProperty, nameof(_viewBaseContext.Padding));
			Assert.That(_viewBaseContext.Padding == _view.Padding);
			_view.Padding = new UIPadding(0);
			Assert.That(_viewBaseContext.Padding == _view.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.PaddingProperty, nameof(_viewBaseContext.Padding), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.Padding == _view.Padding);
			_viewBaseContext.Padding = new UIPadding(0);
			Assert.That(_viewBaseContext.Padding == _view.Padding);
			_view.Padding = new UIPadding(1);
			Assert.That(_viewBaseContext.Padding == _view.Padding);
		}
	}
}