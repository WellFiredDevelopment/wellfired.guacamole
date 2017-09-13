using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class CornerMaskTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextCornerMask()
		{
			_view.CornerMask = CornerMask.Bottom;
			_context.CornerMask = CornerMask.BottomLeft;
			Assert.That(_context.CornerMask != _view.CornerMask);
			_view.Bind(Views.View.CornerMaskProperty, nameof(_context.CornerMask));
			Assert.That(_context.CornerMask == _view.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.CornerMaskProperty, nameof(_context.CornerMask), BindingMode.OneWay);
			Assert.That(_context.CornerMask == _view.CornerMask);
			_context.CornerMask = CornerMask.Bottom;
			Assert.That(_context.CornerMask == _view.CornerMask);
			_view.CornerMask = CornerMask.BottomLeft;
			Assert.That(_context.CornerMask != _view.CornerMask);
		}
		
		[Test]
		public void ViewBaseCornerMaskBindingDoesntWorkInTwoWayWithReadOnlyMode()
		{
			_view.Bind(Views.View.CornerMaskProperty, nameof(_context.CornerMask), BindingMode.ReadOnly);
			Assert.That(_context.CornerMask == _view.CornerMask);
			
			_context.CornerMask = CornerMask.Bottom;
			Assert.That(_context.CornerMask == _view.CornerMask);
			
			_view.CornerMask = CornerMask.BottomLeft;
			Assert.That(_view.CornerMask != CornerMask.BottomLeft);
			Assert.That(_context.CornerMask == _view.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingWorksInOneWay()
		{
			_view.Bind(Views.View.CornerMaskProperty, nameof(_context.CornerMask));
			Assert.That(_context.CornerMask == _view.CornerMask);
			_context.CornerMask = CornerMask.Bottom;
			Assert.That(_context.CornerMask == _view.CornerMask);
		}

		[Test]
		public void ViewBaseCornerMaskBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.CornerMaskProperty, nameof(_context.CornerMask), BindingMode.TwoWay);
			Assert.That(_context.CornerMask == _view.CornerMask);
			_context.CornerMask = CornerMask.Bottom;
			Assert.That(_context.CornerMask == _view.CornerMask);
			_view.CornerMask = CornerMask.BottomLeft;
			Assert.That(_context.CornerMask == _view.CornerMask);
		}
	}
}