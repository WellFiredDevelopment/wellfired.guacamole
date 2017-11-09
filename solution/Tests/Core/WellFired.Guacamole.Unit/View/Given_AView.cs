using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.View
{
    [TestFixture]
    public class GivenAView
    {   
        [Test]
        public void That_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = Substitute.For<IView>();
            view.HorizontalLayout.Returns(LayoutOptions.Fill);
            view.VerticalLayout.Returns(LayoutOptions.Fill);
            view.Content.Returns(default(IView));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 100)));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 100, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 100, 100)));
        }

        [Test]
        public void That_HasASpecifiedMinSize_And_IsLayedoutTwice_Then_LayoutIsDoneCorrectly()
        {
            var view = Substitute.For<IView>();
            view.HorizontalLayout.Returns(LayoutOptions.Expand);
            view.VerticalLayout.Returns(LayoutOptions.Expand);
            view.MinSize.Returns(UISize.Of(50, 10));
            view.Content.Returns(default(IView));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 500, 500));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 50, 10)));

            ViewSizingExtensions.DoSizingAndLayout(view, UIRect.With(0, 0, 500, 100));
            Assert.That(view.RectRequest, Is.EqualTo(UIRect.With(0, 0, 50, 10)));
        }
        
        [Test]
        public void When_A_View_Is_Instanciated_It_Gets_A_Unitque_Identifier()
        {
            const int viewCount = 1000;
            var hashset = new HashSet<string>();
            for (int i = 0; i < viewCount; i++)
            {
                hashset.Add(new Views.View().Id);
            }

            Assert.AreEqual(viewCount, hashset.Count);
        }
    }
}