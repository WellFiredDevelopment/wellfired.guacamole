using WellFired.Guacamole.Databinding;

namespace WellFired.Guacamole
{
	public class Label : ViewBase
	{
		public static readonly BindableProperty TextProperty = BindableProperty.Create<Label, string>(
			defaultValue: string.Empty,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.Text
		);

		public static readonly BindableProperty TextColorProperty = BindableProperty.Create<Label, UIColor>(
			defaultValue: UIColor.Black,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.TextColor
		);

		public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty.Create<Label, UITextAlign>(
			defaultValue: UITextAlign.Start,
			bindingMode: BindingMode.TwoWay,
			getter: entry => entry.HorizontalTextAlign
		);

		public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty.Create<Label, UITextAlign>(
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

		public Label()
		{
			BackgroundColor = UIColor.FromRGB(84, 84, 84);
			OutlineColor = BackgroundColor;
			TextColor = UIColor.White;
			HorizontalTextAlign = UITextAlign.Middle;
			VerticalTextAlign = UITextAlign.Middle;
		}

		protected override UIRect CalculateValidRectRequest()
		{
			return new UIRect(0, 0, 100, 20);
		}
	}
}