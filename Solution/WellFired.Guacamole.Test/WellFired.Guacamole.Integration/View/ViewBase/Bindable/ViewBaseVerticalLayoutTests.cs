using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseVerticalLayoutTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextVerticalLayoutOptions()
		{
			_view.VerticalLayout = LayoutOptions.Expand;
			_viewBaseContext.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.VerticalLayoutOptions != _view.VerticalLayout);
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_viewBaseContext.VerticalLayoutOptions));
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _view.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_viewBaseContext.VerticalLayoutOptions));
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _view.VerticalLayout);
			_viewBaseContext.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _view.VerticalLayout);
			_view.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.VerticalLayoutOptions != _view.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingWorksInOneWay()
		{
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_viewBaseContext.VerticalLayoutOptions));
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _view.VerticalLayout);
			_view.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _view.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_viewBaseContext.VerticalLayoutOptions),
				BindingMode.TwoWay);
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _view.VerticalLayout);
			_viewBaseContext.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _view.VerticalLayout);
			_view.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.VerticalLayoutOptions == _view.VerticalLayout);
		}
	}
}