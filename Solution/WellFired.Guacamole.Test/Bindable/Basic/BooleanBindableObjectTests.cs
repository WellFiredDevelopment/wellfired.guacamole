using NUnit.Framework;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Test.Bindable.Basic
{
	[TestFixture]
	public class BooleanBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty BooleanProperty = BindableProperty.Create<BindableTestObject, bool>(
					defaultValue: false,
					bindingMode: BindingMode.TwoWay,
					getter: testObject => testObject.Value
				);

			public bool Value
			{
				get { return (bool)GetValue(BooleanProperty); }
				set { SetValue(BooleanProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private bool _currentValue;

			public bool Value
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
			source.Bind(BindableTestObject.BooleanProperty, nameof(ContextObject.Value));
			bindingContext.Value = true;

			Assert.AreEqual(source.Value, bindingContext.Value);
		}

		[Test]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.BooleanProperty, nameof(ContextObject.Value));
			bindingContext.Value = true;

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = false;

			Assert.AreNotEqual(bindingContext.Value, source.Value);
		}

		[Test]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.BooleanProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = true;

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = false;

			Assert.AreEqual(bindingContext.Value, source.Value);
		}
	}
}