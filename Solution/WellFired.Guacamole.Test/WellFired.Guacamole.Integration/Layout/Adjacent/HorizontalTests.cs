using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Tests.Integration.Layout.Adjacent
{
	[TestFixture]
	public class HorizontalTests
	{
		[Test]
		public void LayoutWithNoSpacingTwoElement()
		{
			var adjacentLayout = new AdjacentLayout
			{
				Orientation = OrientationOptions.Horizontal,
				Spacing = 0,
				Children =
				{
					new Label { MinSize = new UISize(50, 50) },
					new Label { MinSize = new UISize(50, 50) }
				}
			};

			adjacentLayout.CalculateRectRequest();
			adjacentLayout.AttemptToFullfillRequests(new UIRect(0, 0, 100, 100));
			adjacentLayout.UpdateContextIfNeeded();

			var rectRequest0 = adjacentLayout.Children[0].RectRequest;
			Assert.That(rectRequest0.X, Is.EqualTo(0));
			Assert.That(rectRequest0.Y, Is.EqualTo(0));
			Assert.That(rectRequest0.Width, Is.EqualTo(50));
			Assert.That(rectRequest0.Height, Is.EqualTo(50));
			var rectRequest1 = adjacentLayout.Children[1].RectRequest;
			Assert.That(rectRequest1.X, Is.EqualTo(50));
			Assert.That(rectRequest1.Y, Is.EqualTo(0));
			Assert.That(rectRequest1.Width, Is.EqualTo(50));
			Assert.That(rectRequest1.Height, Is.EqualTo(50));
		}

		[Test]
		public void LayoutWithNoSpacingThreeElements()
		{
			var adjacentLayout = new AdjacentLayout
			{
				Orientation = OrientationOptions.Horizontal,
				Spacing = 0,
				Children =
				{
					new Label { MinSize = new UISize(50, 50) },
					new Label { MinSize = new UISize(50, 50) },
					new Label { MinSize = new UISize(50, 50) }
				}
			};

			adjacentLayout.CalculateRectRequest();
			adjacentLayout.AttemptToFullfillRequests(new UIRect(0, 0, 150, 150));
			adjacentLayout.UpdateContextIfNeeded();

			var rectRequest0 = adjacentLayout.Children[0].RectRequest;
			Assert.That(rectRequest0.X, Is.EqualTo(0));
			Assert.That(rectRequest0.Y, Is.EqualTo(0));
			Assert.That(rectRequest0.Width, Is.EqualTo(50));
			Assert.That(rectRequest0.Height, Is.EqualTo(50));
			var rectRequest1 = adjacentLayout.Children[1].RectRequest;
			Assert.That(rectRequest1.X, Is.EqualTo(50));
			Assert.That(rectRequest1.Y, Is.EqualTo(0));
			Assert.That(rectRequest1.Width, Is.EqualTo(50));
			Assert.That(rectRequest1.Height, Is.EqualTo(50));
			var rectRequest2 = adjacentLayout.Children[2].RectRequest;
			Assert.That(rectRequest2.X, Is.EqualTo(100));
			Assert.That(rectRequest2.Y, Is.EqualTo(0));
			Assert.That(rectRequest2.Width, Is.EqualTo(50));
			Assert.That(rectRequest2.Height, Is.EqualTo(50));
		}
	}
}