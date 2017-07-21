using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Unit.Bindable
{
	[TestFixture]
	public class IntBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty IntProperty = BindableProperty.Create<BindableTestObject, int>(
				0,
				BindingMode.TwoWay,
				testObject => testObject.Value
			);

			public int Value
			{
				get { return (int) GetValue(IntProperty); }
				set { SetValue(IntProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private int _currentValue;

			public int Value
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
			source.Bind(BindableTestObject.IntProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 15;

			Assert.That(bindingContext.Value, Is.Not.EqualTo(source.Value));
		}

		[Test]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.IntProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.IntProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = 10;

			Assert.That(source.Value, Is.EqualTo(bindingContext.Value));

			source.Value = 5;

			Assert.That(bindingContext.Value, Is.EqualTo(source.Value));
		}
	}
}