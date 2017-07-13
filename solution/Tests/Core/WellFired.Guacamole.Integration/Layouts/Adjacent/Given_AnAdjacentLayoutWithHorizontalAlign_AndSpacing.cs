using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Adjacent
{
	[TestFixture]
	public class Given_AnAdjacentLayoutWithHorizontalAlign_AndSpacing
	{
	    [Test]
	    public void And_ItHasTwoChildren_And_ItHasAParentView_When_Layout_Then_LayoutIsCorrect()
	    {
	        var adjacentLayout = new LayoutView
	        {
		        HorizontalLayout = LayoutOptions.Expand,
		        VerticalLayout = LayoutOptions.Expand,
	            Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 5),
	            Padding = UIPadding.Of(10),
	            Children =
	            {
	                new Label { MinSize = UISize.Of(50) },
	                new Label { MinSize = UISize.Of(50) }
	            }
	        };

	        var view = new Views.View {
	            HorizontalLayout = LayoutOptions.Fill,
	            VerticalLayout = LayoutOptions.Fill,
	            Padding = UIPadding.Of(10),
	            Content = adjacentLayout
	        };

	        ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(500, 500));

	        var viewRect = view.RectRequest;
	        Assert.That(viewRect, Is.EqualTo(UIRect.With(500, 500)));

	        Assert.That(view.Content.RectRequest.X, Is.EqualTo(10));
	        Assert.That(view.Content.RectRequest.Y, Is.EqualTo(10));
		    Assert.That(view.Content.RectRequest.Width, Is.EqualTo(105));
		    Assert.That(view.Content.RectRequest.Height, Is.EqualTo(50));

	        var child0Rect = adjacentLayout.Children[0].RectRequest;
	        Assert.That(child0Rect.X, Is.EqualTo(10));
	        Assert.That(child0Rect.Y, Is.EqualTo(10));
		    Assert.That(child0Rect.Width, Is.EqualTo(40));
		    Assert.That(child0Rect.Height, Is.EqualTo(30));
		    
		    var child1Rect = adjacentLayout.Children[1].RectRequest;
		    Assert.That(child1Rect.X, Is.EqualTo(55));
		    Assert.That(child1Rect.Y, Is.EqualTo(10));
		    Assert.That(child1Rect.Width, Is.EqualTo(40));
		    Assert.That(child1Rect.Height, Is.EqualTo(30));
	    }
		
		[Test]
		public void And_ItHasThreeChildren_And_ItHasAParentView_When_Layout_Then_LayoutIsCorrect()
		{
			var adjacentLayout = new LayoutView
			{
				HorizontalLayout = LayoutOptions.Expand,
				VerticalLayout = LayoutOptions.Expand,
				Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 5),
				Padding = UIPadding.Of(10),
				Children =
				{
					new Label { MinSize = UISize.Of(50) },
					new Label { MinSize = UISize.Of(50) },
					new Label { MinSize = UISize.Of(50) }
				}
			};

			var view = new Views.View {
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Fill,
				Padding = UIPadding.Of(10),
				Content = adjacentLayout
			};

			ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(500, 500));

			var viewRect = view.RectRequest;
			Assert.That(viewRect, Is.EqualTo(UIRect.With(500, 500)));

			Assert.That(view.Content.RectRequest.X, Is.EqualTo(10));
			Assert.That(view.Content.RectRequest.Y, Is.EqualTo(10));
			Assert.That(view.Content.RectRequest.Width, Is.EqualTo(160));
			Assert.That(view.Content.RectRequest.Height, Is.EqualTo(50));

			// Width is calculated using these values == total width - padding. I calculate it here so that it's consistent
			// with the math run on another platform. It also takes into account spacing.
			const int width = (160 - 20 - 5 * 2) / 3;
			
			var child0Rect = adjacentLayout.Children[0].RectRequest;
			Assert.That(child0Rect.X, Is.EqualTo(10));
			Assert.That(child0Rect.Y, Is.EqualTo(10));
			Assert.That(child0Rect.Width, Is.EqualTo(width));
			Assert.That(child0Rect.Height, Is.EqualTo(30));
		    
			var child1Rect = adjacentLayout.Children[1].RectRequest;
			Assert.That(child1Rect.X, Is.EqualTo(10 + 5 + width));
			Assert.That(child1Rect.Y, Is.EqualTo(10));
			Assert.That(child1Rect.Width, Is.EqualTo(width));
			Assert.That(child1Rect.Height, Is.EqualTo(30));
		    
			var child2Rect = adjacentLayout.Children[2].RectRequest;
			Assert.That(child2Rect.X, Is.EqualTo(10 + 5 + width + 5 + width));
			Assert.That(child2Rect.Y, Is.EqualTo(10));
			Assert.That(child2Rect.Width, Is.EqualTo(width));
			Assert.That(child2Rect.Height, Is.EqualTo(30));
		}
	}
}