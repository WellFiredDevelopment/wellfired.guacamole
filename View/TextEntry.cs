using WellFired.Guacamole.Databinding;

namespace WellFired.Guacamole
{
	public class TextEntry : ViewBase
	{
		public static readonly BindableProperty TextProperty = BindableProperty.Create<TextEntry, string>(
			defaultValue: string.Empty,
			bindingMode: BindingMode.OneWay,
			getter: entry => entry.Text
		);

		public static readonly BindableProperty LabelProperty = BindableProperty.Create<TextEntry, string>(
			defaultValue: string.Empty,
			bindingMode: BindingMode.OneWay,
			getter: entry => entry.Label
		);

		public string Label
		{
			get { return (string)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 250, 20);
		}
	}
}