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

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create<TextEntry, UIColor>(
			defaultValue: UIColor.Black,
			bindingMode: BindingMode.OneWay,
			getter: entry => entry.TextColor
		);

		public static readonly BindableProperty LabelColorProperty = BindableProperty.Create<TextEntry, UIColor>(
			defaultValue: UIColor.Black,
			bindingMode: BindingMode.OneWay,
			getter: entry => entry.LabelColor
		);

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public UIColor TextColor
		{
			get { return (UIColor)GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		public UIColor LabelColor
		{
			get { return (UIColor)GetValue(LabelColorProperty); }
			set { SetValue(LabelColorProperty, value); }
		}

		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 250, 20);
		}
	}
}