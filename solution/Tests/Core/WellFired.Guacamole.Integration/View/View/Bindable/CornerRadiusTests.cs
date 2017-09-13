using System;
using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Integration.View.View.Bindable
{
	[TestFixture]
	public class CornerRadiusTests
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
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextCornerRadius()
		{
			_view.CornerRadius = 0.0f;
			_context.CornerRadius = 1.0f;
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) > 0.01f);
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_context.CornerRadius));
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_context.CornerRadius), BindingMode.OneWay);
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
			_context.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
			_view.CornerRadius = 3.0f;
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) > 0.01f);
		}
		
		[Test]
		public void ViewBaseCornerRadiusBindingDoesntWorkInTwoWayWithReadOnlyMode()
		{
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_context.CornerRadius), BindingMode.OneWay);
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
			_context.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
			_view.CornerRadius = 3.0f;
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) > 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingWorksInOneWay()
		{
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_context.CornerRadius));
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
			_context.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_context.CornerRadius),
				BindingMode.TwoWay);
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
			_context.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
			_view.CornerRadius = 3.0f;
			Assert.That(Math.Abs(_context.CornerRadius - _view.CornerRadius) < 0.01f);
		}
	}
}