using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class ViewBaseMinSizeTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextMinSize()
		{
			_view.MinSize = UISize.One;
			_context.MinSize = UISize.Min;
			Assert.That(_context.MinSize != _view.MinSize);
			_view.Bind(Views.View.MinSizeProperty, nameof(_context.MinSize));
			Assert.That(_context.MinSize == _view.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.MinSizeProperty, nameof(_context.MinSize));
			Assert.That(_context.MinSize == _view.MinSize);
			_context.MinSize = UISize.One;
			Assert.That(_context.MinSize == _view.MinSize);
			_view.MinSize = UISize.Min;
			Assert.That(_context.MinSize != _view.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingWorksInOneWay()
		{
			_view.Bind(Views.View.MinSizeProperty, nameof(_context.MinSize));
			Assert.That(_context.MinSize == _view.MinSize);
			_context.MinSize = UISize.One;
			Assert.That(_context.MinSize == _view.MinSize);
		}

		[Test]
		public void ViewBaseMinSizeBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.MinSizeProperty, nameof(_context.MinSize), BindingMode.TwoWay);
			Assert.That(_context.MinSize == _view.MinSize);
			_context.MinSize = UISize.One;
			Assert.That(_context.MinSize == _view.MinSize);
			_view.MinSize = UISize.Min;
			Assert.That(_context.MinSize == _view.MinSize);
		}
	}
}