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

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create<TextEntry, UIColor>(
			defaultValue: UIColor.Black,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.TextColor
		);

		public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty.Create<TextEntry, UITextAlign>(
			defaultValue: UITextAlign.Start,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.HorizontalTextAlign
		);

		public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty.Create<TextEntry, UITextAlign>(
			defaultValue: UITextAlign.Middle,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.VerticalTextAlign
		);

		public float Number
		{
			get { return (float)GetValue(NumberProperty); }
			set { SetValue(NumberProperty, value); }
		}

		public UIColor TextColor
		{
			get { return (UIColor)GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		public UITextAlign HorizontalTextAlign
		{
			get { return (UITextAlign)GetValue(HorizontalTextAlignProperty); }
			set { SetValue(HorizontalTextAlignProperty, value); }
		}

		public UITextAlign VerticalTextAlign
		{
			get { return (UITextAlign)GetValue(VerticalTextAlignProperty); }
			set { SetValue(VerticalTextAlignProperty, value); }
		}

		public NumberEntry()
		{
			BackgroundColor = UIColor.FromRGB(66, 66, 66);
			OutlineColor = BackgroundColor;
			TextColor = UIColor.White;
		}

		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 100, 20);
		}
	}
}