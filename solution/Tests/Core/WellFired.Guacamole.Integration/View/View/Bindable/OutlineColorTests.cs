using NUnit.Framework;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class ViewBaseOutlineColorTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextOutlineColor()
		{
			_view.OutlineColor = UIColor.Blue;
			_context.OutlineColor = UIColor.Red;
			Assert.That(_context.OutlineColor != _view.OutlineColor);
			_view.Bind(Views.View.OutlineColorProperty, nameof(_context.OutlineColor));
			Assert.That(_context.OutlineColor == _view.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.OutlineColorProperty, nameof(_context.OutlineColor));
			Assert.That(_context.OutlineColor == _view.OutlineColor);
			_context.OutlineColor = UIColor.Blue;
			Assert.That(_context.OutlineColor == _view.OutlineColor);
			_view.OutlineColor = UIColor.Red;
			Assert.That(_context.OutlineColor != _view.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingWorksInOneWay()
		{
			_view.Bind(Views.View.OutlineColorProperty, nameof(_context.OutlineColor));
			Assert.That(_context.OutlineColor == _view.OutlineColor);
			_context.OutlineColor = UIColor.Brown;
			Assert.That(_context.OutlineColor == _view.OutlineColor);
		}

		[Test]
		public void ViewBaseOutlineColorBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.OutlineColorProperty, nameof(_context.OutlineColor),
				BindingMode.TwoWay);
			Assert.That(_context.OutlineColor == _view.OutlineColor);
			_context.OutlineColor = UIColor.Blue;
			Assert.That(_context.OutlineColor == _view.OutlineColor);
			_view.OutlineColor = UIColor.Red;
			Assert.That(_context.OutlineColor == _view.OutlineColor);
		}
	}
}