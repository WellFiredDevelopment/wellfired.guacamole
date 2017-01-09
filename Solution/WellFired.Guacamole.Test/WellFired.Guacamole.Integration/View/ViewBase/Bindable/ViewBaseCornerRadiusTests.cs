using System;
using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Tests.Integration.View.ViewBase.Bindable
{
	[TestFixture]
	public class ViewBaseCornerRadiusTests
	{
		[SetUp]
		public void Setup()
		{
			_view = new Views.View();
			_viewBaseContext = new ViewBaseContextObject();
			_view.BindingContext = _viewBaseContext;
		}

		private Views.View _view;
		private ViewBaseContextObject _viewBaseContext;

		[Test]
		public void OnBindViewBaseIsAutomaticallyUpdatedToTheValueOfBindingContextCornerRadius()
		{
			_view.CornerRadius = 0.0f;
			_viewBaseContext.CornerRadius = 1.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) > 0.01f);
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_viewBaseContext.CornerRadius));
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) < 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingDoesntWorkInTwoWayWithOneWayMode()
		{
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_viewBaseContext.CornerRadius));
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) < 0.01f);
			_viewBaseContext.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) < 0.01f);
			_view.CornerRadius = 3.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) > 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingWorksInOneWay()
		{
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_viewBaseContext.CornerRadius));
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) < 0.01f);
			_viewBaseContext.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) < 0.01f);
		}

		[Test]
		public void ViewBaseCornerRadiusBindingWorksInTwoWay()
		{
			_view.Bind(Views.View.CornerRadiusProperty, nameof(_viewBaseContext.CornerRadius),
				BindingMode.TwoWay);
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) < 0.01f);
			_viewBaseContext.CornerRadius = 2.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) < 0.01f);
			_view.CornerRadius = 3.0f;
			Assert.That(Math.Abs(_viewBaseContext.CornerRadius - _view.CornerRadius) < 0.01f);
		}
	}
}