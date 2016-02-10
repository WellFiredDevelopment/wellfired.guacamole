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

		public string Label
		{
			get;
			set;
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