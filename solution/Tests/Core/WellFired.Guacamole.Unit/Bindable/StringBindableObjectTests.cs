using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Bindable
{
	[TestFixture]
	public class StringBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty TextProperty = BindableProperty.Create<BindableTestObject, string>(
				string.Empty,
				BindingMode.TwoWay,
				testObject => testObject.Value
			);

			public string Value
			{
				get { return (string) GetValue(TextProperty); }
				set { SetValue(TextProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private string _currentValue = string.Empty;

			public string Value
			{
				get { return _currentValue; }
				set { SetProperty(ref _currentValue, value); }
			}
		}

		[Test]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.TextProperty, nameof(ContextObject.Value), BindingMode.OneWay);
			bindingContext.Value = "NewText";

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = "NewText2";
			Assert.That(bindingContext.Value, Is.Not.EqualTo(source.Value));
		}

		[Test]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.TextProperty, nameof(ContextObject.Value));
			bindingContext.Value = "NewText";

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}
		
		[Test]
		public void ReadOnlyBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.TextProperty, nameof(ContextObject.Value), BindingMode.ReadOnly);
			
			bindingContext.Value = "NewText";

			Assert.That(bindingContext.Value, Is.EqualTo("NewText"));
			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = "OtherText";
			Assert.That(source.Value, Is.EqualTo("NewText"));
			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.TextProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = "NewText";

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = "NewText2";

			Assert.That(bindingContext.Value, Is.EqualTo(source.Value));
		}
	}
}