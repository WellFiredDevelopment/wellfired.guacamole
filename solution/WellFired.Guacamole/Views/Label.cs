using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
	public class Label : View
	{
		[PublicAPI] public static readonly BindableProperty FontSizeProperty = BindableProperty.Create<Label, int>(
			12,
			BindingMode.TwoWay,
			v => v.FontSize
		);
		
		[PublicAPI] public static readonly BindableProperty WordWrapProperty = BindableProperty.Create<Label, bool>(
			true,
			BindingMode.TwoWay,
			v => v.WordWrap
		);
		
		[PublicAPI] public static readonly BindableProperty ClippingProperty = BindableProperty.Create<Label, UITextClipping>(
			UITextClipping.Clip,
			BindingMode.TwoWay,
			v => v.Clipping
		);
		
		[PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<Label, string>(
			string.Empty,
			BindingMode.TwoWay,
			v => v.Text
		);

		[PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty.Create<Label, UIColor>(
			UIColor.Black,
			BindingMode.TwoWay,
			v => v.TextColor
		);

		[PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
			.Create<Label, UITextAlign>(
				UITextAlign.Start,
				BindingMode.TwoWay,
				v => v.HorizontalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
			.Create<Label, UITextAlign>(
				UITextAlign.Middle,
				BindingMode.TwoWay,
				v => v.VerticalTextAlign
			);

		public Label()
		{
			Style = Styling.Styles.Label.Style;
		}

		[PublicAPI]
		public int FontSize
		{
			get => (int) GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
		}

		[PublicAPI]
		public bool WordWrap
		{
			get => (bool) GetValue(WordWrapProperty);
			set => SetValue(WordWrapProperty, value);
		}
		
		[PublicAPI]
		public UITextClipping Clipping
		{
			get => (UITextClipping) GetValue(ClippingProperty);
			set => SetValue(ClippingProperty, value);
		}

		[PublicAPI]
		public string Text
		{
			get => (string) GetValue(TextProperty);
			set => SetValue(TextProperty, value);
		}

		[PublicAPI]
		public UIColor TextColor
		{
			get => (UIColor) GetValue(TextColorProperty);
			set => SetValue(TextColorProperty, value);
		}

		[PublicAPI]
		public UITextAlign HorizontalTextAlign
		{
			get => (UITextAlign) GetValue(HorizontalTextAlignProperty);
			set => SetValue(HorizontalTextAlignProperty, value);
		}

		[PublicAPI]
		public UITextAlign VerticalTextAlign
		{
			get => (UITextAlign) GetValue(VerticalTextAlignProperty);
			set => SetValue(VerticalTextAlignProperty, value);
		}
	}
}