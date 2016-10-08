using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBasePaddingTests
	{
		private Guacamole.View.ViewBase _viewBase;
		private ViewBaseContextObject _viewBaseContext;

		[SetUp]
		public void OneTimeSetup()
		{
			_viewBase = new Guacamole.View.ViewBase();
			_viewBaseContext = new ViewBaseContextObject();
			_viewBase.BindingContext = _viewBaseContext;
		}

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextPadding()
		{
			_viewBase.Padding = new UIPadding(0);
			_viewBaseContext.Padding = new UIPadding(1);
			Assert.That(_viewBaseContext.Padding != _viewBase.Padding);
			_viewBase.Bind(Guacamole.View.ViewBase.PaddingProperty, nameof(_viewBaseContext.Padding));
			Assert.That(_viewBaseContext.Padding == _viewBase.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingWorksInOneWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.PaddingProperty, nameof(_viewBaseContext.Padding));
			Assert.That(_viewBaseContext.Padding == _viewBase.Padding);
			_viewBase.Padding = new UIPadding(0);
			Assert.That(_viewBaseContext.Padding == _viewBase.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingWorksInTwoWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.PaddingProperty, nameof(_viewBaseContext.Padding), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.Padding == _viewBase.Padding);
			_viewBaseContext.Padding = new UIPadding(0);
			Assert.That(_viewBaseContext.Padding == _viewBase.Padding);
			_viewBase.Padding = new UIPadding(1);
			Assert.That(_viewBaseContext.Padding == _viewBase.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.PaddingProperty, nameof(_viewBaseContext.Padding));
			Assert.That(_viewBaseContext.Padding == _viewBase.Padding);
			_viewBaseContext.Padding = new UIPadding(0);
			Assert.That(_viewBaseContext.Padding == _viewBase.Padding);
			_viewBase.Padding = new UIPadding(1);
			Assert.That(_viewBaseContext.Padding != _viewBase.Padding);
		}
	}
}