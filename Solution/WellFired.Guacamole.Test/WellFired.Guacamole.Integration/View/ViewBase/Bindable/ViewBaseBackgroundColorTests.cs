using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseBackgroundColorTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextBackgroundColor()
		{
			_view.BackgroundColor = UIColor.Blue;
			_viewBaseContext.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor != _view.BackgroundColor);
			_view.Bind(Views.View.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _view.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _view.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Blue;
			Assert.That(_viewBaseContext.BackgroundColor == _view.BackgroundColor);
			_view.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor != _view.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInOneWay()
		{
			_view.Bind(Views.View.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _view.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Brown;
			Assert.That(_viewBaseContext.BackgroundColor == _view.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor),
				BindingMode.TwoWay);
			Assert.That(_viewBaseContext.BackgroundColor == _view.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Blue;
			Assert.That(_viewBaseContext.BackgroundColor == _view.BackgroundColor);
			_view.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor == _view.BackgroundColor);
		}
	}
}