using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseCornerMaskTests
	{
		[SetUp]
		public void Setup()
		{
			_viewBase = new Guacamole.View.ViewBase();
			_viewBaseContext = new ViewBaseContextObject();
			_viewBase.BindingContext = _viewBaseContext;
		}

		private Guacamole.View.ViewBase _viewBase;
		private ViewBaseContextObject _viewBaseContext;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextCornerMask()
		{
			_viewBase.CornerMask = CornerMask.Bottom;
			_viewBaseContext.CornerMask = CornerMask.BottomLeft;
			Assert.That(_viewBaseContext.CornerMask != _viewBase.CornerMask);
			_viewBase.Bind(Guacamole.View.ViewBase.CornerMaskProperty, nameof(_viewBaseContext.CornerMask));
			Assert.That(_viewBaseContext.CornerMask == _viewBase.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.CornerMaskProperty, nameof(_viewBaseContext.CornerMask), BindingMode.OneWay);
			Assert.That(_viewBaseContext.CornerMask == _viewBase.CornerMask);
			_viewBaseContext.CornerMask = CornerMask.Bottom;
			Assert.That(_viewBaseContext.CornerMask == _viewBase.CornerMask);
			_viewBase.CornerMask = CornerMask.BottomLeft;
			Assert.That(_viewBaseContext.CornerMask != _viewBase.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingWorksInOneWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.CornerMaskProperty, nameof(_viewBaseContext.CornerMask));
			Assert.That(_viewBaseContext.CornerMask == _viewBase.CornerMask);
			_viewBaseContext.CornerMask = CornerMask.Bottom;
			Assert.That(_viewBaseContext.CornerMask == _viewBase.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingWorksInTwoWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.CornerMaskProperty, nameof(_viewBaseContext.CornerMask), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.CornerMask == _viewBase.CornerMask);
			_viewBaseContext.CornerMask = CornerMask.Bottom;
			Assert.That(_viewBaseContext.CornerMask == _viewBase.CornerMask);
			_viewBase.CornerMask = CornerMask.BottomLeft;
			Assert.That(_viewBaseContext.CornerMask == _viewBase.CornerMask);
		}
	}
}