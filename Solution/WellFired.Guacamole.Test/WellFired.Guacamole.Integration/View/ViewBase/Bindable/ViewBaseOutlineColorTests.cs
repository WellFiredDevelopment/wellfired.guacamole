using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseOutlineColorTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextOutlineColor()
		{
			_view.OutlineColor = UIColor.Blue;
			_viewBaseContext.OutlineColor = UIColor.Red;
			Assert.That(_viewBaseContext.OutlineColor != _view.OutlineColor);
			_view.Bind(Views.View.OutlineColorProperty, nameof(_viewBaseContext.OutlineColor));
			Assert.That(_viewBaseContext.OutlineColor == _view.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.OutlineColorProperty, nameof(_viewBaseContext.OutlineColor));
			Assert.That(_viewBaseContext.OutlineColor == _view.OutlineColor);
			_viewBaseContext.OutlineColor = UIColor.Blue;
			Assert.That(_viewBaseContext.OutlineColor == _view.OutlineColor);
			_view.OutlineColor = UIColor.Red;
			Assert.That(_viewBaseContext.OutlineColor != _view.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingWorksInOneWay()
		{
			_view.Bind(Views.View.OutlineColorProperty, nameof(_viewBaseContext.OutlineColor));
			Assert.That(_viewBaseContext.OutlineColor == _view.OutlineColor);
			_viewBaseContext.OutlineColor = UIColor.Brown;
			Assert.That(_viewBaseContext.OutlineColor == _view.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.OutlineColorProperty, nameof(_viewBaseContext.OutlineColor),
				BindingMode.TwoWay);
			Assert.That(_viewBaseContext.OutlineColor == _view.OutlineColor);
			_viewBaseContext.OutlineColor = UIColor.Blue;
			Assert.That(_viewBaseContext.OutlineColor == _view.OutlineColor);
			_view.OutlineColor = UIColor.Red;
			Assert.That(_viewBaseContext.OutlineColor == _view.OutlineColor);
		}
	}
}