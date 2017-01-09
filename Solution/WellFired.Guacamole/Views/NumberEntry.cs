using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.Views
{
	public class NumberEntry : View, ITypeable
	{
		[PublicAPI] public static readonly BindableProperty NumberProperty = BindableProperty.Create<NumberEntry, float>
		(
			10.0f,
			BindingMode.TwoWay,
			entry => entry.Number
		);

		[PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty
			.Create<TextEntry, UIColor>(
				UIColor.Black,
				BindingMode.TwoWay,
				entry => entry.TextColor
			);

		[PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
			.Create<TextEntry, UITextAlign>(
				UITextAlign.Start,
				BindingMode.TwoWay,
				entry => entry.HorizontalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
			.Create<TextEntry, UITextAlign>(
				UITextAlign.Middle,
				BindingMode.TwoWay,
				entry => entry.VerticalTextAlign
			);

		public NumberEntry()
		{
			BackgroundColor = UIColor.FromRGB(66, 66, 66);
			OutlineColor = BackgroundColor;
			TextColor = UIColor.White;
		}

		[PublicAPI]
		public float Number
		{
			get { return (float) GetValue(NumberProperty); }
			set { SetValue(NumberProperty, value); }
		}

		[PublicAPI]
		public UIColor TextColor
		{
			get { return (UIColor) GetValue(TextColorProperty); }
			set { SetValue(TextColorProperty, value); }
		}

		[PublicAPI]
		public UITextAlign HorizontalTextAlign
		{
			get { return (UITextAlign) GetValue(HorizontalTextAlignProperty); }
			set { SetValue(HorizontalTextAlignProperty, value); }
		}

		[PublicAPI]
		public UITextAlign VerticalTextAlign
		{
			get { return (UITextAlign) GetValue(VerticalTextAlignProperty); }
			set { SetValue(VerticalTextAlignProperty, value); }
		}

		public void Type(char key)
		{
			Number = (float)char.GetNumericValue(key);
		}
	}
}