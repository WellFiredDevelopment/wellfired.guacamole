using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseMinSizeTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextMinSize()
		{
			_viewBase.MinSize = UISize.One;
			_viewBaseContext.MinSize = UISize.Min;
			Assert.That(_viewBaseContext.MinSize != _viewBase.MinSize);
			_viewBase.Bind(Guacamole.View.ViewBase.MinSizeProperty, nameof(_viewBaseContext.MinSize));
			Assert.That(_viewBaseContext.MinSize == _viewBase.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.MinSizeProperty, nameof(_viewBaseContext.MinSize));
			Assert.That(_viewBaseContext.MinSize == _viewBase.MinSize);
			_viewBaseContext.MinSize = UISize.One;
			Assert.That(_viewBaseContext.MinSize == _viewBase.MinSize);
			_viewBase.MinSize = UISize.Min;
			Assert.That(_viewBaseContext.MinSize != _viewBase.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingWorksInOneWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.MinSizeProperty, nameof(_viewBaseContext.MinSize));
			Assert.That(_viewBaseContext.MinSize == _viewBase.MinSize);
			_viewBaseContext.MinSize = UISize.One;
			Assert.That(_viewBaseContext.MinSize == _viewBase.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingWorksInTwoWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.MinSizeProperty, nameof(_viewBaseContext.MinSize), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.MinSize == _viewBase.MinSize);
			_viewBaseContext.MinSize = UISize.One;
			Assert.That(_viewBaseContext.MinSize == _viewBase.MinSize);
			_viewBase.MinSize = UISize.Min;
			Assert.That(_viewBaseContext.MinSize == _viewBase.MinSize);
		}
	}
}