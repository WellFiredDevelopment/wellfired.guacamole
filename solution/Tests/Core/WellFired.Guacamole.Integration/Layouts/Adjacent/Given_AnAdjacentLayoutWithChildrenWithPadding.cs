using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Adjacent
{
	[TestFixture]
	public class Given_AnAdjacentLayoutWithChildrenWithPadding
	{
	    [Test]
	    public void When_Layout_Then_LayoutIsCorrect()
	    {
		    var child0 = new Label
		    {
			    MinSize = UISize.Of(50),
			    Padding = UIPadding.Of(5),
			    HorizontalLayout = LayoutOptions.Expand,
			    VerticalLayout = LayoutOptions.Expand
		    };
		    
		    var child1 = new Label
		    {
			    MinSize = UISize.Of(50),
			    Padding = UIPadding.Of(5),
			    HorizontalLayout = LayoutOptions.Expand,
			    VerticalLayout = LayoutOptions.Expand
		    };
		    
	        var adjacentLayout = new LayoutView
	        {
	            Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 0),
		        Padding = UIPadding.Of(0),
		        HorizontalLayout = LayoutOptions.Fill,
		        VerticalLayout = LayoutOptions.Fill,
	            Children =
	            {
	                child0,
	                child1
	            }
	        };

	        ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

	        Assert.That(adjacentLayout.RectRequest, Is.EqualTo(UIRect.With(500, 500)));

	        var child0Rect = adjacentLayout.Children[0].RectRequest;
	        Assert.That(child0Rect.X, Is.EqualTo(0));
	        Assert.That(child0Rect.Y, Is.EqualTo(0));
		    Assert.That(child0Rect.Width, Is.EqualTo(60));
		    Assert.That(child0Rect.Height, Is.EqualTo(60));
		    
		    var child1Rect = adjacentLayout.Children[1].RectRequest;
		    Assert.That(child1Rect.X, Is.EqualTo(60));
		    Assert.That(child1Rect.Y, Is.EqualTo(0));
		    Assert.That(child1Rect.Width, Is.EqualTo(60));
		    Assert.That(child1Rect.Height, Is.EqualTo(60));
	    }
		
		[Test]
		public void With_Spacing_When_Layout_Then_LayoutIsCorrect()
		{
			var child0 = new Label
			{
				MinSize = UISize.Of(50),
				Padding = UIPadding.Of(5),
				HorizontalLayout = LayoutOptions.Expand,
				VerticalLayout = LayoutOptions.Expand
			};
		    
			var child1 = new Label
			{
				MinSize = UISize.Of(50),
				Padding = UIPadding.Of(5),
				HorizontalLayout = LayoutOptions.Expand,
				VerticalLayout = LayoutOptions.Expand
			};
		    
			var adjacentLayout = new LayoutView
			{
				Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 5),
				Padding = UIPadding.Of(0),
				HorizontalLayout = LayoutOptions.Fill,
				VerticalLayout = LayoutOptions.Fill,
				Children =
				{
					child0,
					child1
				}
			};

			ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

			Assert.That(adjacentLayout.RectRequest, Is.EqualTo(UIRect.With(500, 500)));

			var child0Rect = adjacentLayout.Children[0].RectRequest;
			Assert.That(child0Rect.X, Is.EqualTo(0));
			Assert.That(child0Rect.Y, Is.EqualTo(0));
			Assert.That(child0Rect.Width, Is.EqualTo(60));
			Assert.That(child0Rect.Height, Is.EqualTo(60));
		    
			var child1Rect = adjacentLayout.Children[1].RectRequest;
			Assert.That(child1Rect.X, Is.EqualTo(65));
			Assert.That(child1Rect.Y, Is.EqualTo(0));
			Assert.That(child1Rect.Width, Is.EqualTo(60));
			Assert.That(child1Rect.Height, Is.EqualTo(60));
		}
	}
}