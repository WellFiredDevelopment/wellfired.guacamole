using NUnit.Framework;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Unit.Converter
{
	public class ConvertTest
	{
		[Test]
		public void Test1()
		{
			var bindableObject = new YesNoBindingContext();
			var label = new LabelView { Text = "Invalid" };
			Assert.That(label.Text, Is.EqualTo("Invalid"));

			label.BindingContext = bindableObject;
			label.Bind(LabelView.TextProperty, "YesNo", new YesNoConverter());
			Assert.That(label.Text, Is.EqualTo("no"));

			bindableObject.YesNo = true;
			Assert.That(label.Text, Is.EqualTo("yes"));

			label.Text = "no";
			Assert.That(bindableObject.YesNo, Is.EqualTo(false));
		}
		
		[Test]
		public void Test2()
		{
			var bindableObject = new PlurialBindingContext();
			var label = new LabelView { Text = "Invalid" };
			Assert.That(label.Text, Is.EqualTo("Invalid"));

			label.BindingContext = bindableObject;
			label.Bind(LabelView.TextProperty, "Word", new PlurialConverter());
			Assert.That(label.Text, Is.EqualTo("dogs"));

			bindableObject.Word = "dogs";
			Assert.That(label.Text, Is.EqualTo("dog"));

			label.Text = "dogs";
			Assert.That(bindableObject.Word, Is.EqualTo("dog"));
		}
	}
}