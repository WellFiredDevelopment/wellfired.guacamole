using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseCornerMaskTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextCornerMask()
		{
			_view.CornerMask = CornerMask.Bottom;
			_viewBaseContext.CornerMask = CornerMask.BottomLeft;
			Assert.That(_viewBaseContext.CornerMask != _view.CornerMask);
			_view.Bind(Views.View.CornerMaskProperty, nameof(_viewBaseContext.CornerMask));
			Assert.That(_viewBaseContext.CornerMask == _view.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.CornerMaskProperty, nameof(_viewBaseContext.CornerMask), BindingMode.OneWay);
			Assert.That(_viewBaseContext.CornerMask == _view.CornerMask);
			_viewBaseContext.CornerMask = CornerMask.Bottom;
			Assert.That(_viewBaseContext.CornerMask == _view.CornerMask);
			_view.CornerMask = CornerMask.BottomLeft;
			Assert.That(_viewBaseContext.CornerMask != _view.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingWorksInOneWay()
		{
			_view.Bind(Views.View.CornerMaskProperty, nameof(_viewBaseContext.CornerMask));
			Assert.That(_viewBaseContext.CornerMask == _view.CornerMask);
			_viewBaseContext.CornerMask = CornerMask.Bottom;
			Assert.That(_viewBaseContext.CornerMask == _view.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.CornerMaskProperty, nameof(_viewBaseContext.CornerMask), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.CornerMask == _view.CornerMask);
			_viewBaseContext.CornerMask = CornerMask.Bottom;
			Assert.That(_viewBaseContext.CornerMask == _view.CornerMask);
			_view.CornerMask = CornerMask.BottomLeft;
			Assert.That(_viewBaseContext.CornerMask == _view.CornerMask);
		}
	}
}