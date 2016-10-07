using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.UI.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseVerticalLayoutTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextVerticalLayoutOptions()
		{
			_viewBase.VerticalLayout = LayoutOptions.Expand;
			_viewBaseContext.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.VerticalLayoutOptions != _viewBase.VerticalLayout);
			_viewBase.Bind(View.ViewBase.VerticalLayoutProperty, nameof(_viewBaseContext.VerticalLayoutOptions));
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _viewBase.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingWorksInOneWay()
		{
			_viewBase.Bind(View.ViewBase.VerticalLayoutProperty, nameof(_viewBaseContext.VerticalLayoutOptions));
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _viewBase.VerticalLayout);
			_viewBase.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _viewBase.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingWorksInTwoWay()
		{
			_viewBase.Bind(View.ViewBase.VerticalLayoutProperty, nameof(_viewBaseContext.VerticalLayoutOptions), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _viewBase.VerticalLayout);
			_viewBaseContext.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _viewBase.VerticalLayout);
			_viewBase.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _viewBase.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(View.ViewBase.VerticalLayoutProperty, nameof(_viewBaseContext.VerticalLayoutOptions));
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _viewBase.VerticalLayout);
			_viewBaseContext.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _viewBase.VerticalLayout);
			_viewBase.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.VerticalLayoutOptions != _viewBase.VerticalLayout);
		}
	}
}