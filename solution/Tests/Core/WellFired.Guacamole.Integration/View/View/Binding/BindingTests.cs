using NUnit.Framework;
using WellFired.Guacamole.Integration.View.View.Bindable;

namespace WellFired.Guacamole.Integration.View.View.Binding
{
    [TestFixture]
    public class BindingTests
    {
        [Test]
        public void WithOneView_OnBindingContextSetAfterConstruction_Then_BindingContextIsSetIsCorrect()
        {
            var context = new ContextObject();
            var view = new Views.View();
            
            view.BindingContext = context;
            Assert.That(view.BindingContext, Is.EqualTo(context));
        }
        
        [Test]
        public void WithOneViewWithContent_OnBindingContextSetBeforeContent_Then_BindingContextIsCorrect()
        {
            var context = new ContextObject();
            var view = new Views.View();
            var content = new Views.View();
            
            view.BindingContext = context;
            view.Content = content;
            
            Assert.That(view.BindingContext, Is.EqualTo(context));
            Assert.That(content.BindingContext, Is.EqualTo(context));
        }
        
        [Test]
        public void WithOneViewWithContentWithContext_OnBindingContextSetBeforeContent_Then_BindingContextIsSetIsCorrect()
        {
            var context0 = new ContextObject();
            var context1 = new ContextObject();
            var view = new Views.View();
            var content = new Views.View();

            view.BindingContext = context0;
            content.BindingContext = context1;
            view.Content = content;
            
            Assert.That(view.BindingContext, Is.EqualTo(context0));
            Assert.That(content.BindingContext, Is.EqualTo(context1));
        }
        
        [Test]
        public void WithOneViewWithContent_OnBindingContextSetAfterContent_Then_BindingContextIsCorrect()
        {
            var context = new ContextObject();
            var view = new Views.View();
            var content = new Views.View();
            
            view.Content = content;
            view.BindingContext = context;
            
            Assert.That(view.BindingContext, Is.EqualTo(context));
            Assert.That(content.BindingContext, Is.EqualTo(context));
        }
        
        [Test]
        public void WithOneViewWithContentWithContext_OnBindingContextSetAfterContent_Then_BindingContextIsSetIsCorrect()
        {
            var context0 = new ContextObject();
            var context1 = new ContextObject();
            var view = new Views.View();
            var content = new Views.View();

            view.Content = content;
            view.BindingContext = context0;
            content.BindingContext = context1;
            
            Assert.That(view.BindingContext, Is.EqualTo(context0));
            Assert.That(content.BindingContext, Is.EqualTo(context1));
        }
    }
}