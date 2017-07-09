using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Layouts;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Tests.VirtualCell
{
    [TestFixture]
    public class Given_AVirtualCell
    {
        [Test]
        public void With_HEVE_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 20, 20));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Expand);
            layoutable.VerticalLayout.Returns(LayoutOptions.Expand);

            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };

            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(0, 0, 20, 20)));
        }

        [Test]
        public void With_HEVF_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 20, 100));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Expand);
            layoutable.VerticalLayout.Returns(LayoutOptions.Fill);
                
            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };
            
            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(0, 0, 20, 100)));
        }
        
        [Test]
        public void With_HFVE_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 100, 20));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Fill);
            layoutable.VerticalLayout.Returns(LayoutOptions.Expand);
                
            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };
            
            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(0, 0, 100, 20)));
        }
        
        [Test]
        public void With_HFVF_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 100, 100));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Fill);
            layoutable.VerticalLayout.Returns(LayoutOptions.Fill);
                
            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };
            
            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(0, 0, 100, 100)));
        }
        
        [Test]
        public void With_HEVC_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 20, 20));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Expand);
            layoutable.VerticalLayout.Returns(LayoutOptions.Center);
                
            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };
            
            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(0, 40, 20, 20)));
        }
        
        [Test]
        public void With_HCVE_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 20, 20));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Center);
            layoutable.VerticalLayout.Returns(LayoutOptions.Expand);
                
            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };
            
            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(40, 0, 20, 20)));
        }
        
        [Test]
        public void With_HCVC_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 20, 20));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Center);
            layoutable.VerticalLayout.Returns(LayoutOptions.Center);
                
            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };
            
            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(40, 40, 20, 20)));
        }
        
        [Test]
        public void With_HFVC_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 100, 20));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Fill);
            layoutable.VerticalLayout.Returns(LayoutOptions.Center);
                
            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };
            
            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(0, 40, 100, 20)));
        }
        
        [Test]
        public void With_HCVF_And_PositionHasBeenCalculated_Then_Position_IsCorrect()
        {
            var layoutable = Substitute.For<ILayoutable>();
            layoutable.RectRequest.Returns(UIRect.With(0, 0, 20, 100));
            layoutable.HorizontalLayout.Returns(LayoutOptions.Center);
            layoutable.VerticalLayout.Returns(LayoutOptions.Fill);
                
            var cell = new Layouts.VirtualCell
            {
                Layoutable = layoutable,
                Rect = UIRect.With(0, 0, 100, 100)
            };
            
            cell.CalculatePositionInCell();

            Assert.That(cell.PositionInCell, Is.EqualTo(UIRect.With(40, 0, 20, 100)));
        }
    }
}