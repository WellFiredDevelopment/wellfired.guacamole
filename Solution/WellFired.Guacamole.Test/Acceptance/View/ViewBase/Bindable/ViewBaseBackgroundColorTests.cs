using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseBackgroundColorTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextBackgroundColor()
		{
			_viewBase.BackgroundColor = UIColor.Blue;
			_viewBaseContext.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor != _viewBase.BackgroundColor);
			_viewBase.Bind(Guacamole.View.ViewBase.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Blue;
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBase.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor != _viewBase.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInOneWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Brown;
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInTwoWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor),
				BindingMode.TwoWay);
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Blue;
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBase.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
		}
	}
}