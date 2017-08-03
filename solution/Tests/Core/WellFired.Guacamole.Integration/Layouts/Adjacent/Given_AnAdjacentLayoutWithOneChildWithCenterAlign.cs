using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Integration.Layouts.Adjacent
{
    public class Given_AnAdjacentLayoutWithOneChildWithCenterAlign
    {
        [Test]
        public void When_HorizontalLayout_WithoutSpacing_Then_LayoutIsCorrect()
        {
            var child = new Label {
                Padding = UIPadding.Zero,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                MinSize = UISize.Of(40, 40)
            };
            
            var adjacentLayout = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 0, LayoutOptions.Center, LayoutOptions.Center),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children = {
                    child
                }
            };

            ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(500));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(500));
            
            Assert.That(child.RectRequest.X, Is.EqualTo(230));
            Assert.That(child.RectRequest.Y, Is.EqualTo(230));
            Assert.That(child.RectRequest.Width, Is.EqualTo(40));
            Assert.That(child.RectRequest.Height, Is.EqualTo(40));
        }
        
        [Test]
        public void When_HorizontalLayout_WithSpacing_Then_LayoutIsCorrect()
        {
            var child = new Label {
                Padding = UIPadding.Zero,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                MinSize = UISize.Of(40, 40)
            };
            
            var adjacentLayout = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Horizontal, 20, LayoutOptions.Center, LayoutOptions.Center),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children = {
                    child
                }
            };

            ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(500));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(500));
            
            Assert.That(child.RectRequest.X, Is.EqualTo(230));
            Assert.That(child.RectRequest.Y, Is.EqualTo(230));
            Assert.That(child.RectRequest.Width, Is.EqualTo(40));
            Assert.That(child.RectRequest.Height, Is.EqualTo(40));
        }
        
        [Test]
        public void When_VerticalLayout_WithoutSpacing_Then_LayoutIsCorrect()
        {
            var child = new Label {
                Padding = UIPadding.Zero,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                MinSize = UISize.Of(40, 40)
            };
            
            var adjacentLayout = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical, 0, LayoutOptions.Center, LayoutOptions.Center),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children = {
                    child
                }
            };

            ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(500));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(500));
            
            Assert.That(child.RectRequest.X, Is.EqualTo(230));
            Assert.That(child.RectRequest.Y, Is.EqualTo(230));
            Assert.That(child.RectRequest.Width, Is.EqualTo(40));
            Assert.That(child.RectRequest.Height, Is.EqualTo(40));
        }
        
        [Test]
        public void When_VerticalLayout_WithSpacing_Then_LayoutIsCorrect()
        {
            var child = new Label {
                Padding = UIPadding.Zero,
                HorizontalLayout = LayoutOptions.Expand,
                VerticalLayout = LayoutOptions.Expand,
                MinSize = UISize.Of(40, 40)
            };
            
            var adjacentLayout = new LayoutView
            {
                Layout = AdjacentLayout.Of(OrientationOptions.Vertical, 20, LayoutOptions.Center, LayoutOptions.Center),
                HorizontalLayout = LayoutOptions.Fill,
                VerticalLayout = LayoutOptions.Fill,
                Children = {
                    child
                }
            };

            ViewSizingExtensions.DoSizingAndLayout(adjacentLayout, UIRect.With(500, 500));

            Assert.That(adjacentLayout.RectRequest.Width, Is.EqualTo(500));
            Assert.That(adjacentLayout.RectRequest.Height, Is.EqualTo(500));
            
            Assert.That(child.RectRequest.X, Is.EqualTo(230));
            Assert.That(child.RectRequest.Y, Is.EqualTo(230));
            Assert.That(child.RectRequest.Width, Is.EqualTo(40));
            Assert.That(child.RectRequest.Height, Is.EqualTo(40));
        }
    }
}