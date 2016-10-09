using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Test.Acceptance.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseMaxSizeTests
	{
		[SetUp]
		public void OneTimeSetup()
		{
			_viewBase = new Guacamole.View.ViewBase();
			_viewBaseContext = new ViewBaseContextObject();
			_viewBase.BindingContext = _viewBaseContext;
		}

		private Guacamole.View.ViewBase _viewBase;
		private ViewBaseContextObject _viewBaseContext;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextMaxSize()
		{
			_viewBase.MaxSize = UISize.One;
			_viewBaseContext.MaxSize = UISize.Min;
			Assert.That(_viewBaseContext.MaxSize != _viewBase.MaxSize);
			_viewBase.Bind(Guacamole.View.ViewBase.MaxSizeProperty, nameof(_viewBaseContext.MaxSize));
			Assert.That(_viewBaseContext.MaxSize == _viewBase.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.MaxSizeProperty, nameof(_viewBaseContext.MaxSize));
			Assert.That(_viewBaseContext.MaxSize == _viewBase.MaxSize);
			_viewBaseContext.MaxSize = UISize.One;
			Assert.That(_viewBaseContext.MaxSize == _viewBase.MaxSize);
			_viewBase.MaxSize = UISize.Min;
			Assert.That(_viewBaseContext.MaxSize != _viewBase.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingWorksInOneWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.MaxSizeProperty, nameof(_viewBaseContext.MaxSize));
			Assert.That(_viewBaseContext.MaxSize == _viewBase.MaxSize);
			_viewBaseContext.MaxSize = UISize.One;
			Assert.That(_viewBaseContext.MaxSize == _viewBase.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingWorksInTwoWay()
		{
			_viewBase.Bind(Guacamole.View.ViewBase.MaxSizeProperty, nameof(_viewBaseContext.MaxSize), BindingMode.TwoWay);
			Assert.That(_viewBaseContext.MaxSize == _viewBase.MaxSize);
			_viewBaseContext.MaxSize = UISize.One;
			Assert.That(_viewBaseContext.MaxSize == _viewBase.MaxSize);
			_viewBase.MaxSize = UISize.Min;
			Assert.That(_viewBaseContext.MaxSize == _viewBase.MaxSize);
		}
	}
}