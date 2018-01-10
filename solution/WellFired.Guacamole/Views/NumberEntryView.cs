using WellFired.Guacamole.Data;
using JetBrains.Annotations;
using WellFired.Guacamole.DataBinding;

namespace WellFired.Guacamole.Views
{
	public class NumberEntryView : View, ITypeable
	{
		[PublicAPI] public static readonly BindableProperty NumberProperty = BindableProperty.Create<NumberEntryView, float>
		(
			10.0f,
			BindingMode.TwoWay,
			entry => entry.Number
		);

		[PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty
			.Create<TextEntryView, UIColor>(
				UIColor.Black,
				BindingMode.TwoWay,
				entry => entry.TextColor
			);

		[PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
			.Create<TextEntryView, UITextAlign>(
				UITextAlign.Start,
				BindingMode.TwoWay,
				entry => entry.HorizontalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
			.Create<TextEntryView, UITextAlign>(
				UITextAlign.Middle,
				BindingMode.TwoWay,
				entry => entry.VerticalTextAlign
			);

		public NumberEntryView()
		{
			BackgroundColor = UIColor.FromRGB(66, 66, 66);
			OutlineColor = BackgroundColor;
			TextColor = UIColor.White;
		}

		[PublicAPI]
		public float Number
		{
			get => (float) GetValue(NumberProperty);
			set => SetValue(NumberProperty, value);
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

		public void Type(char key)
		{
			Number = (float)char.GetNumericValue(key);
		}
	}
}