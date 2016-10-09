using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseOutlineColorTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextOutlineColor()
		{
			_viewBase.OutlineColor = UIColor.Blue;
			_viewBaseContext.OutlineColor = UIColor.Red;
			Assert.That(_viewBaseContext.OutlineColor != _viewBase.OutlineColor);
			_viewBase.Bind(Guacamole.View.ViewBase.OutlineColorProperty, nameof(_viewBaseContext.OutlineColor));
			Assert.That(_viewBaseContext.OutlineColor == _viewBase.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.OutlineColorProperty, nameof(_viewBaseContext.OutlineColor));
			Assert.That(_viewBaseContext.OutlineColor == _viewBase.OutlineColor);
			_viewBaseContext.OutlineColor = UIColor.Blue;
			Assert.That(_viewBaseContext.OutlineColor == _viewBase.OutlineColor);
			_viewBase.OutlineColor = UIColor.Red;
			Assert.That(_viewBaseContext.OutlineColor != _viewBase.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingWorksInOneWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.OutlineColorProperty, nameof(_viewBaseContext.OutlineColor));
			Assert.That(_viewBaseContext.OutlineColor == _viewBase.OutlineColor);
			_viewBaseContext.OutlineColor = UIColor.Brown;
			Assert.That(_viewBaseContext.OutlineColor == _viewBase.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingWorksInTwoWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.OutlineColorProperty, nameof(_viewBaseContext.OutlineColor),
				BindingMode.TwoWay);
			Assert.That(_viewBaseContext.OutlineColor == _viewBase.OutlineColor);
			_viewBaseContext.OutlineColor = UIColor.Blue;
			Assert.That(_viewBaseContext.OutlineColor == _viewBase.OutlineColor);
			_viewBase.OutlineColor = UIColor.Red;
			Assert.That(_viewBaseContext.OutlineColor == _viewBase.OutlineColor);
		}
	}
}