using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class VerticalLayoutTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextVerticalLayoutOptions()
		{
			_view.VerticalLayout = LayoutOptions.Expand;
			_context.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_context.VerticalLayoutOptions != _view.VerticalLayout);
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_context.VerticalLayoutOptions));
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_context.VerticalLayoutOptions), BindingMode.OneWay);
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
			_context.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
			_view.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_context.VerticalLayoutOptions != _view.VerticalLayout);
		}
		
		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingDoesntWorkInTwoWayWithReadOnlyMode()
		{
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_context.VerticalLayoutOptions), BindingMode.ReadOnly);
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
			
			_context.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
			
			_view.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_view.VerticalLayout != LayoutOptions.Expand);
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingWorksInOneWay()
		{
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_context.VerticalLayoutOptions));
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
			_view.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
		}

		[Test]
		public void ViewBaseVerticalLayoutOptionsBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.VerticalLayoutProperty, nameof(_context.VerticalLayoutOptions),
				BindingMode.TwoWay);
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
			_context.VerticalLayoutOptions = LayoutOptions.Fill;
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
			_view.VerticalLayout = LayoutOptions.Expand;
			Assert.That(_context.VerticalLayoutOptions == _view.VerticalLayout);
		}
	}
}