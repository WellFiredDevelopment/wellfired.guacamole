using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseMaxSizeTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextMaxSize()
		{
			_view.MaxSize = UISize.One;
			_viewBaseContext.MaxSize = UISize.Min;
			Assert.That(_viewBaseContext.MaxSize != _view.MaxSize);
			_view.Bind(Views.View.MaxSizeProperty, nameof(_viewBaseContext.MaxSize));
			Assert.That(_viewBaseContext.MaxSize == _view.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.MaxSizeProperty, nameof(_viewBaseContext.MaxSize));
			Assert.That(_viewBaseContext.MaxSize == _view.MaxSize);
			_viewBaseContext.MaxSize = UISize.One;
			Assert.That(_viewBaseContext.MaxSize == _view.MaxSize);
			_view.MaxSize = UISize.Min;
			Assert.That(_viewBaseContext.MaxSize != _view.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingWorksInOneWay()
		{
			_view.Bind(Views.View.MaxSizeProperty, nameof(_viewBaseContext.MaxSize));
			Assert.That(_viewBaseContext.MaxSize == _view.MaxSize);
			_viewBaseContext.MaxSize = UISize.One;
			Assert.That(_viewBaseContext.MaxSize == _view.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.MaxSizeProperty, nameof(_viewBaseContext.MaxSize), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.MaxSize == _view.MaxSize);
			_viewBaseContext.MaxSize = UISize.One;
			Assert.That(_viewBaseContext.MaxSize == _view.MaxSize);
			_view.MaxSize = UISize.Min;
			Assert.That(_viewBaseContext.MaxSize == _view.MaxSize);
		}
	}
}