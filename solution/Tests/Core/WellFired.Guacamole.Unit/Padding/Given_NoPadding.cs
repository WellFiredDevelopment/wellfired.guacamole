﻿using NUnit.Framework;
using WellFired.Guacamole.Types;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Padding
{
    [TestFixture]
    public class GivenNoPadding
    {
        [Test]
        public void When_AdjustingAViewWithHEVE_Then_ReturnedRectIsCorrect()
        {
            var size = UISize.Of(100, 100);
            var padding = UIPadding.Of(0);
            const LayoutOptions horizontalLayout = LayoutOptions.Expand;
            const LayoutOptions verticalLayout = LayoutOptions.Expand;
            
            var returnedRect = ViewPaddingCalculation.AdjustForPadding(horizontalLayout, verticalLayout, padding, size);
            
            Assert.That(returnedRect, Is.EqualTo(UISize.Of(100, 100)));
        }
        
        [Test]
        public void When_AdjustingAViewWithHEVF_Then_ReturnedRectIsCorrect()
        {
            var size = UISize.Of(100, 100);
            var padding = UIPadding.Of(0);
            const LayoutOptions horizontalLayout = LayoutOptions.Expand;
            const LayoutOptions verticalLayout = LayoutOptions.Fill;
            
            var returnedRect = ViewPaddingCalculation.AdjustForPadding(horizontalLayout, verticalLayout, padding, size);
            
            Assert.That(returnedRect, Is.EqualTo(UISize.Of(100, 100)));
        }
        
        [Test]
        public void When_AdjustingAViewWithHFVE_Then_ReturnedRectIsCorrect()
        {
            var size = UISize.Of(100, 100);
            var padding = UIPadding.Of(0);
            const LayoutOptions horizontalLayout = LayoutOptions.Fill;
            const LayoutOptions verticalLayout = LayoutOptions.Expand;
            
            var returnedRect = ViewPaddingCalculation.AdjustForPadding(horizontalLayout, verticalLayout, padding, size);
            
            Assert.That(returnedRect, Is.EqualTo(UISize.Of(100, 100)));
        }
        
        [Test]
        public void When_AdjustingAViewWithHFVF_Then_ReturnedRectIsCorrect()
        {
            var size = UISize.Of(100, 100);
            var padding = UIPadding.Of(0);
            const LayoutOptions horizontalLayout = LayoutOptions.Fill;
            const LayoutOptions verticalLayout = LayoutOptions.Fill;
            
            var returnedRect = ViewPaddingCalculation.AdjustForPadding(horizontalLayout, verticalLayout, padding, size);
            
            Assert.That(returnedRect, Is.EqualTo(UISize.Of(100, 100)));
        }
        
        [Test]
        public void When_AdjustingAViewWithHCVC_Then_ReturnedRectIsCorrect()
        {
            var size = UISize.Of(100, 100);
            var padding = UIPadding.Of(0);
            const LayoutOptions horizontalLayout = LayoutOptions.Center;
            const LayoutOptions verticalLayout = LayoutOptions.Center;
            
            var returnedRect = ViewPaddingCalculation.AdjustForPadding(horizontalLayout, verticalLayout, padding, size);
            
            Assert.That(returnedRect, Is.EqualTo(UISize.Of(100, 100)));
        }
    }
}