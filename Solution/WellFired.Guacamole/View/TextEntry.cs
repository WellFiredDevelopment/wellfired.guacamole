﻿using WellFired.Guacamole.Annotations;
using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Types;

namespace WellFired.Guacamole.View
{
	public class TextEntry : ViewBase, ITypeable
	{
		[PublicAPI] public static readonly BindableProperty TextProperty = BindableProperty.Create<TextEntry, string>(
			string.Empty,
			BindingMode.TwoWay,
			entry => entry.Text
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

		public TextEntry()
		{
			// Set some nice defaults
			BackgroundColor = UIColor.FromRGB(66, 66, 66);
			OutlineColor = BackgroundColor;
			TextColor = UIColor.White;
			HorizontalLayout = LayoutOptions.Fill;
			CornerRadius = 8;
			Padding = new UIPadding(5, 5, 5, 5);
			VerticalTextAlign = UITextAlign.Middle;
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

		public void Type(char key)
		{
			Text += key;
		}
	}
}