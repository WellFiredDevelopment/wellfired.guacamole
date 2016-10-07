using Microsoft.VisualStudio.TestTools.UnitTesting;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Test.Bindable.Basic
{
	[TestClass]
	public class DoubleBindableObjectTests
	{
		private class BindableTestObject : BindableObject
		{
			public static readonly BindableProperty DoubleProperty = BindableProperty.Create<BindableTestObject, double>(
					defaultValue: 0.0,
					bindingMode: BindingMode.TwoWay,
					getter: testObject => testObject.Value
				);

			public double Value
			{
				get { return (double)GetValue(DoubleProperty); }
				set { SetValue(DoubleProperty, value); }
			}
		}

		private class ContextObject : NotifyBase
		{
			private double _currentValue = 0.0;

			public double Value
			{
				get { return _currentValue; }
				set { SetProperty(ref _currentValue, value, nameof(Value)); }
			}
		}

		[TestMethod]
		public void OneWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10.0;

			Assert.AreEqual(source.Value, bindingContext.Value);
		}

		[TestMethod]
		public void OneWayBindingInverseTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value));
			bindingContext.Value = 10.0;

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = 15.0;

			Assert.AreNotEqual(bindingContext.Value, source.Value);
		}

		[TestMethod]
		public void TwoWayBindingTest()
		{
			var source = new BindableTestObject();
			var bindingContext = new ContextObject();
			source.BindingContext = bindingContext;
			source.Bind(BindableTestObject.DoubleProperty, nameof(ContextObject.Value), BindingMode.TwoWay);
			bindingContext.Value = 10.0;

			Assert.AreEqual(source.Value, bindingContext.Value);

			source.Value = 15.0;

			Assert.AreEqual(bindingContext.Value, source.Value);
		}
	}
}