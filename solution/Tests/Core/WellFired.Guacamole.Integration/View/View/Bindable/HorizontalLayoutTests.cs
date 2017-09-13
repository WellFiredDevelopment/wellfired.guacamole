using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class HorizontalLayoutTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextHorizontalLayoutOptions()
		{
			_view.HorizontalLayout = LayoutOptions.Expand;
			_context.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_context.HorizontalLayoutOptions != _view.HorizontalLayout);
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_context.HorizontalLayoutOptions));
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_context.HorizontalLayoutOptions), BindingMode.OneWay);
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
			_context.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
			_view.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_context.HorizontalLayoutOptions != _view.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingDoesntWorkInTwoWayWithReadOnlyMode()
		{
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_context.HorizontalLayoutOptions), BindingMode.ReadOnly);
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
			
			_context.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
			
			_view.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_view.HorizontalLayout != LayoutOptions.Expand);
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
		}
		
		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingWorksInOneWay()
		{
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_context.HorizontalLayoutOptions));
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
			_view.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
		}

		[Test]
		public void ViewBaseHorizontalLayoutOptionsBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.HorizontalLayoutProperty, nameof(_context.HorizontalLayoutOptions),
				BindingMode.TwoWay);
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
			_context.HorizontalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
			_view.HorizontalLayout = LayoutOptions.Expand;
			Assert.That(_context.HorizontalLayoutOptions == _view.HorizontalLayout);
		}
	}
}