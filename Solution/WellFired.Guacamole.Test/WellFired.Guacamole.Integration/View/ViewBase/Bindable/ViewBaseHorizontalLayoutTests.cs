using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseHorizontalLayoutTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextHorizontalLayoutOptions()
		{
			_view.HorizontalLayout = LayoutOptions.Expand;
			_viewBaseContext.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions != _view.HorizontalLayout);
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_viewBaseContext.HorizontalLayoutOptions));
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _view.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_viewBaseContext.HorizontalLayoutOptions));
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _view.HorizontalLayout);
			_viewBaseContext.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _view.HorizontalLayout);
			_view.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions != _view.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingWorksInOneWay()
		{
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_viewBaseContext.HorizontalLayoutOptions));
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _view.HorizontalLayout);
			_view.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _view.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_viewBaseContext.HorizontalLayoutOptions),
				BindingMode.TwoWay);
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _view.HorizontalLayout);
			_viewBaseContext.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _view.HorizontalLayout);
			_view.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_viewBaseContext.HorizontalLayoutOptions == _view.HorizontalLayout);
		}
	}
}