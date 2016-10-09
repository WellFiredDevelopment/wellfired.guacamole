using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseHorizontalLayoutTests
	{
		[SetUp]
		public void OneTimeSetup()
		{
			_viewBase = new Guacamole.View.ViewBase();
			_viewBaseContext = new ViewBaseContextObject();
			_viewBase.BindingContext = _viewBaseContext;
		}

		private Guacamole.View.ViewBase _viewBase;
		private ViewBaseContextObject _viewBaseContext;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextHorizontalLayoutOptions()
		{
			_viewBase.HorizontalLayout = LayoutOptions.Expand;
			_viewBaseContext.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions != _viewBase.HorizontalLayout);
			_viewBase.Bind(Guacamole.View.ViewBase.HorizontalLayoutProperty, nameof(_viewBaseContext.HorizontalLayoutOptions));
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _viewBase.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.HorizontalLayoutProperty, nameof(_viewBaseContext.HorizontalLayoutOptions));
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _viewBase.HorizontalLayout);
			_viewBaseContext.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _viewBase.HorizontalLayout);
			_viewBase.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions != _viewBase.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingWorksInOneWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.HorizontalLayoutProperty, nameof(_viewBaseContext.HorizontalLayoutOptions));
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _viewBase.HorizontalLayout);
			_viewBase.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _viewBase.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingWorksInTwoWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.HorizontalLayoutProperty, nameof(_viewBaseContext.HorizontalLayoutOptions),
				BindingMode.TwoWay);
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _viewBase.HorizontalLayout);
			_viewBaseContext.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _viewBase.HorizontalLayout);
			_viewBase.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _viewBase.HorizontalLayout);
		}
	}
}