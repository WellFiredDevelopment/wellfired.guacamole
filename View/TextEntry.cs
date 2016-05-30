using WellFired.Guacamole.Databinding;

namespace WellFired.Guacamole
{
	public class TextEntry : ViewBase
	{
		public static readonly BindableProperty TextProperty = BindableProperty.Create<TextEntry, string>(
			defaultValue: string.Empty,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.Text
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

		public TextEntry()
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