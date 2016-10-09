using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
	public class Label : ViewBase
	{
		[PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<Label, string>(
			string.Empty,
			BindingMode.TwoWay,
			entry => entry.Text
		);

		[PublicAPI] public static readonly BindableProperty TextColorProperty = BindableProperty.Create<Label, UIColor>(
			UIColor.Black,
			BindingMode.TwoWay,
			entry => entry.TextColor
		);

		[PublicAPI] public static readonly BindableProperty HorizontalTextAlignProperty = BindableProperty
			.Create<Label, UITextAlign>(
				UITextAlign.Start,
				BindingMode.TwoWay,
				entry => entry.HorizontalTextAlign
			);

		[PublicAPI] public static readonly BindableProperty VerticalTextAlignProperty = BindableProperty
			.Create<Label, UITextAlign>(
				UITextAlign.Middle,
				BindingMode.TwoWay,
				entry => entry.VerticalTextAlign
			);

		public Label()
		{
			Style = Styling.Styles.Label.Style;
		}

		[PublicAPI]
		public string Text
		{
			get { return (string) GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
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
	}
}