using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Bindable.UI.ViewBase
{
	[TestFixture]
	public class ViewBaseBackgroundColorTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextBackgroundColor()
		{
			_viewBase.BackgroundColor = UIColor.Blue;
			_viewBaseContext.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor != _viewBase.BackgroundColor);
			_viewBase.Bind(View.ViewBase.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInOneWay()
		{
			_viewBase.Bind(View.ViewBase.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Brown;
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingWorksInTwoWay()
		{
			_viewBase.Bind(View.ViewBase.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Blue;
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBase.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
		}

		[Test]
		public void ViewBaseBackgroundColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(View.ViewBase.BackgroundColorProperty, nameof(_viewBaseContext.BackgroundColor));
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBaseContext.BackgroundColor = UIColor.Blue;
			Assert.That(_viewBaseContext.BackgroundColor == _viewBase.BackgroundColor);
			_viewBase.BackgroundColor = UIColor.Red;
			Assert.That(_viewBaseContext.BackgroundColor != _viewBase.BackgroundColor);
		}
	}
}