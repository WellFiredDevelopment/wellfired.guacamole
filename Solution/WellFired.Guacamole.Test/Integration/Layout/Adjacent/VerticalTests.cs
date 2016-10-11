using NUnit.Framework;
using WellFired.Guacamole.Layout;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Test.Integration.Layout.Adjacent
{
	[TestFixture]
	public class VerticalTests
	{
		[Test]
		public void LayoutWithNoSpacingTwoElements()
		{
			var adjacentLayout = new AdjacentLayout
			{
				Orientation = OrientationOptions.Vertical,
				Spacing = 0,
				Children =
				{
					new Label {MinSize = new UISize(50, 50)},
					new Label {MinSize = new UISize(50, 50)}
				}
			};

			adjacentLayout.CalculateRectRequest();
			adjacentLayout.AttemptToFullfillRequests(new UIRect(0, 0, 100, 100));
			adjacentLayout.Layout();

			var rectRequest0 = adjacentLayout.Children[0].RectRequest;
			Assert.That(rectRequest0.X == 0);
			Assert.That(rectRequest0.Y == 0);
			Assert.That(rectRequest0.Width == 50);
			Assert.That(rectRequest0.Height == 50);
			var rectRequest1 = adjacentLayout.Children[1].RectRequest;
			Assert.That(rectRequest1.X == 0);
			Assert.That(rectRequest1.Y == 50);
			Assert.That(rectRequest1.Width == 50);
			Assert.That(rectRequest1.Height == 50);
		}

		[Test]
		public void LayoutWithNoSpacingThreeElements()
		{
			var adjacentLayout = new AdjacentLayout
			{
				Orientation = OrientationOptions.Vertical,
				Spacing = 0,
				Children =
				{
					new Label {MinSize = new UISize(50, 50)},
					new Label {MinSize = new UISize(50, 50)},
					new Label {MinSize = new UISize(50, 50)}
				}
			};

			adjacentLayout.CalculateRectRequest();
			adjacentLayout.AttemptToFullfillRequests(new UIRect(0, 0, 150, 150));
			adjacentLayout.Layout();

			var rectRequest0 = adjacentLayout.Children[0].RectRequest;
			Assert.That(rectRequest0.X == 0);
			Assert.That(rectRequest0.Y == 0);
			Assert.That(rectRequest0.Width == 50);
			Assert.That(rectRequest0.Height == 50);
			var rectRequest1 = adjacentLayout.Children[1].RectRequest;
			Assert.That(rectRequest1.X == 0);
			Assert.That(rectRequest1.Y == 50);
			Assert.That(rectRequest1.Width == 50);
			Assert.That(rectRequest1.Height == 50);
			var rectRequest2 = adjacentLayout.Children[2].RectRequest;
			Assert.That(rectRequest2.X == 0);
			Assert.That(rectRequest2.Y == 100);
			Assert.That(rectRequest2.Width == 50);
			Assert.That(rectRequest2.Height == 50);
		}
	}
}