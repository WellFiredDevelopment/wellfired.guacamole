using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Test.Bindable.Basic
{
	[TestFixture]
	public class IntBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty IntProperty = BindableProperty.Create<BindableTestObject, int>(
					defaultValue: 0,
					bindingMode: BindingMode.TwoWay,
					getter: testObject => testObject.Value
				);

			public int Value
			{
				get { return (int)GetValue(IntProperty); }
				set { SetValue(IntProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private int _currentValue;

			public int Value
			{
				get { return _currentValue; }
				set { SetProperty(ref _currentValue, value, nameof(Value)); }
			}
		}

		[Test]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.IntProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10;

			Assert.AreEqual(source.Value, bindingContext.Value);
		}

		[Test]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.IntProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10;

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = 15;

			Assert.AreNotEqual(bindingContext.Value, source.Value);
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.IntProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = 10;

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = 5;

			Assert.AreEqual(bindingContext.Value, source.Value);
		}
	}
}