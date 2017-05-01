using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layout.Adjacent
{
	[TestFixture]
	public class Given_AnAdjacentLayoutWithHorizontalAlign
	{
	    [Test]
	    public void And_ItHasTwoChildren_And_ItHasAParentView_When_Layout_Then_LayoutIsCorrect()
	    {
	        var adjacentLayout = new LayoutView
	        {
	            Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
	            Padding = UIPadding.Of(10),
	            Spacing = 0,
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

	        var childRect = adjacentLayout.Children[0].RectRequest;
	        Assert.That(childRect.X, Is.EqualTo(10));
	        Assert.That(childRect.Y, Is.EqualTo(10));
	    }

		[Test]
		public void And_ItHasTwoChildren_When_Layout_Then_LayoutIsCorrect()
		{
			var adjacentLayout = new LayoutView
			{
			    Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
			    Spacing = 0,
				Children =
				{
					new Label { MinSize = UISize.Of(50) },
					new Label { MinSize = UISize.Of(50) }
				}
			};

		    ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

			var rectRequest0 = adjacentLayout.Children[0].RectRequest;
		    Assert.That(rectRequest0, Is.EqualTo(UIRect.With(0, 0, 50, 50)));
			var rectRequest1 = adjacentLayout.Children[1].RectRequest;
		    Assert.That(rectRequest1, Is.EqualTo(UIRect.With(50, 0, 50, 50)));
		}

		[Test]
		public void And_ItHasThreeChildren_When_Layout_Then_LayoutIsCorrect()
		{
			var adjacentLayout = new LayoutView
			{
			    Layout = AdjacentLayout.Of(OrientationOptions.Horizontal),
				Spacing = 0,
				Children =
				{
					new Label { MinSize = UISize.Of(50) },
					new Label { MinSize = UISize.Of(50) },
					new Label { MinSize = UISize.Of(50) }
				}
			};

		    ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

		    var rectRequest0 = adjacentLayout.Children[0].RectRequest;
		    Assert.That(rectRequest0, Is.EqualTo(UIRect.With(0, 0, 50, 50)));
		    var rectRequest1 = adjacentLayout.Children[1].RectRequest;
		    Assert.That(rectRequest1, Is.EqualTo(UIRect.With(50, 0, 50, 50)));
		    var rectRequest2 = adjacentLayout.Children[2].RectRequest;
		    Assert.That(rectRequest2, Is.EqualTo(UIRect.With(100, 0, 50, 50)));
		}
	}
}