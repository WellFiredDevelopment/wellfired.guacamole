using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Test.Unit.Bindable
{
	[TestFixture]
	public class FloatBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty FloatProperty = BindableProperty.Create<BindableTestObject, float>(
					defaultValue: 0.0f,
					bindingMode: BindingMode.TwoWay,
					getter: testObject => testObject.Value
				);

			public float Value
			{
				get { return (float)GetValue(FloatProperty); }
				set { SetValue(FloatProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private float _currentValue = 0.0f;

			public float Value
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
			source.Bind(BindableTestObject.FloatProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10.0f;

			Assert.AreEqual(source.Value, bindingContext.Value);
		}

		[Test]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.FloatProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10.0f;

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = 15.0f;

			Assert.AreNotEqual(bindingContext.Value, source.Value);
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.FloatProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = 10.0f;

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = 15.0f;

			Assert.AreEqual(bindingContext.Value, source.Value);
		}
	}
}