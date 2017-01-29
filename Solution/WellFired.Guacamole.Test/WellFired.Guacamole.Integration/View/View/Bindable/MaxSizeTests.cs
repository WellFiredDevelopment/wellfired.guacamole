using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class MaxSizeTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextMaxSize()
		{
			_view.MaxSize = UISize.One;
			_context.MaxSize = UISize.Min;
			Assert.That(_context.MaxSize != _view.MaxSize);
			_view.Bind(Views.View.MaxSizeProperty, nameof(_context.MaxSize));
			Assert.That(_context.MaxSize == _view.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.MaxSizeProperty, nameof(_context.MaxSize));
			Assert.That(_context.MaxSize == _view.MaxSize);
			_context.MaxSize = UISize.One;
			Assert.That(_context.MaxSize == _view.MaxSize);
			_view.MaxSize = UISize.Min;
			Assert.That(_context.MaxSize != _view.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingWorksInOneWay()
		{
			_view.Bind(Views.View.MaxSizeProperty, nameof(_context.MaxSize));
			Assert.That(_context.MaxSize == _view.MaxSize);
			_context.MaxSize = UISize.One;
			Assert.That(_context.MaxSize == _view.MaxSize);
		}

		[Test]
		public void ViewBaseMaxSizeBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.MaxSizeProperty, nameof(_context.MaxSize), BindingMode.TwoWay);
			Assert.That(_context.MaxSize == _view.MaxSize);
			_context.MaxSize = UISize.One;
			Assert.That(_context.MaxSize == _view.MaxSize);
			_view.MaxSize = UISize.Min;
			Assert.That(_context.MaxSize == _view.MaxSize);
		}
	}
}