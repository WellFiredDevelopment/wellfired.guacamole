using WellFired.Guacamole.Databinding;

namespace WellFired.Guacamole
{
	public class NumberEntry : ViewBase
	{
		public static readonly BindableProperty NumberProperty = BindableProperty.Create<NumberEntry, float>(
			defaultValue: 10.0f,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.Number
		);

		public static readonly BindableProperty LabelProperty = BindableProperty.Create<NumberEntry, string>(
			defaultValue: "Name",
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.Label
		);

		public float Number
		{
			get { return (float)GetValue(NumberProperty); }
			set { SetValue(NumberProperty, value); }
		}

		public string Label
		{
			get { return (string)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 250, 20);
		}
	}
}