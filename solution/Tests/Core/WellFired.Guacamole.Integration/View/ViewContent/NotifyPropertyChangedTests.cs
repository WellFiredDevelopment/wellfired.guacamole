using NSubstitute;
using NUnit.Framework;
using WellFired.Guacamole.Integration.View.ViewContent.Binding;

namespace WellFired.Guacamole.Integration.View.ViewContent
{
	[TestFixture]
	public class NotifyPropertyChangedTests
	{
		[Test]
		public void WithOneView_OnBindingContextSetAfterConstruction_Then_PropertyChangedIsRaised()
		{
			var wasRaisedInView = false;
			var wasRaisedInContent = false;
			
			var context = new ContextObject();
			var view = new Views.View();
			var viewContent = Substitute.For<IView>();
			
			view.Content = viewContent;
			
			view.PropertyChanged += (sender, args) => { wasRaisedInView = true; };
			viewContent.PropertyChanged += (sender, args) => { wasRaisedInContent = true; };
				
			view.BindingContext = context;
			
			Assert.That(wasRaisedInView);    
			Assert.That(wasRaisedInContent);
		}
	}
}