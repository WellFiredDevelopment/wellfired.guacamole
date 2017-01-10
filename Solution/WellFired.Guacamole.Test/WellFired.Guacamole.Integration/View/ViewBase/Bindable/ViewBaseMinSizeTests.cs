using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseMinSizeTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextMinSize()
		{
			_view.MinSize = UISize.One;
			_viewBaseContext.MinSize = UISize.Min;
			Assert.That(_viewBaseContext.MinSize != _view.MinSize);
			_view.Bind(Views.View.MinSizeProperty, nameof(_viewBaseContext.MinSize));
			Assert.That(_viewBaseContext.MinSize == _view.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.MinSizeProperty, nameof(_viewBaseContext.MinSize));
			Assert.That(_viewBaseContext.MinSize == _view.MinSize);
			_viewBaseContext.MinSize = UISize.One;
			Assert.That(_viewBaseContext.MinSize == _view.MinSize);
			_view.MinSize = UISize.Min;
			Assert.That(_viewBaseContext.MinSize != _view.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingWorksInOneWay()
		{
			_view.Bind(Views.View.MinSizeProperty, nameof(_viewBaseContext.MinSize));
			Assert.That(_viewBaseContext.MinSize == _view.MinSize);
			_viewBaseContext.MinSize = UISize.One;
			Assert.That(_viewBaseContext.MinSize == _view.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.MinSizeProperty, nameof(_viewBaseContext.MinSize), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.MinSize == _view.MinSize);
			_viewBaseContext.MinSize = UISize.One;
			Assert.That(_viewBaseContext.MinSize == _view.MinSize);
			_view.MinSize = UISize.Min;
			Assert.That(_viewBaseContext.MinSize == _view.MinSize);
		}
	}
}