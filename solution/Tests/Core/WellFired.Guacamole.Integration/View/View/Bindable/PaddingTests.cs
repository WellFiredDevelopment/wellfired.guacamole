using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class PaddingTests
	{
		[SetUp]
		public void Setup()
		{
			_view = new Views.View();
			_context = new ContextObject();
			_view.BindingContext = _context;
		}

		private Views.View _view;
		private ContextObject _context;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextPadding()
		{
			_view.Padding = UIPadding.Zero;
			_context.Padding = UIPadding.One;
			Assert.That(_context.Padding != _view.Padding);
			_view.Bind(Views.View.PaddingProperty, nameof(_context.Padding));
			Assert.That(_context.Padding == _view.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.PaddingProperty, nameof(_context.Padding), BindingMode.OneWay);
			Assert.That(_context.Padding == _view.Padding);
			_context.Padding = UIPadding.Zero;
			Assert.That(_context.Padding == _view.Padding);
			_view.Padding = UIPadding.One;
			Assert.That(_context.Padding != _view.Padding);
		}
		
		[Test]
		public void ViewBasePaddingBindingDoesntWorkInTwoWayWithReadOnlyMode()
		{
			_view.Bind(Views.View.PaddingProperty, nameof(_context.Padding), BindingMode.ReadOnly);
			Assert.That(_context.Padding == _view.Padding);
			
			_context.Padding = UIPadding.Zero;
			Assert.That(_context.Padding == _view.Padding);
			
			_view.Padding = UIPadding.One;
			Assert.That(_view.Padding != UIPadding.One);
			Assert.That(_context.Padding == _view.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingWorksInOneWay()
		{
			_view.Bind(Views.View.PaddingProperty, nameof(_context.Padding));
			Assert.That(_context.Padding == _view.Padding);
			_view.Padding = UIPadding.Zero;
			Assert.That(_context.Padding == _view.Padding);
		}

		[Test]
		public void ViewBasePaddingBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.PaddingProperty, nameof(_context.Padding), BindingMode.TwoWay);
			Assert.That(_context.Padding == _view.Padding);
			_context.Padding = UIPadding.Zero;
			Assert.That(_context.Padding == _view.Padding);
			_view.Padding = UIPadding.One;
			Assert.That(_context.Padding == _view.Padding);
		}
	}
}