using WellFired.Guacamole.Data;

namespace WellFired.Guacamole.Styling.Styles
{
	internal static class TextEntryView
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter {Property = Views.View.BackgroundColorProperty, Value = UIColor.White},
				new Setter {Property = Views.View.OutlineColorProperty, Value = UIColor.Black},
				new Setter {Property = Views.View.OutlineMaskProperty, Value = OutlineMask.Bottom},
				new Setter {Property = Views.View.OutlineThicknessProperty, Value = 1.0},
				new Setter {Property = Views.TextEntryView.TextColorProperty, Value = UIColor.Black},
				new Setter {Property = Views.TextEntryView.PlaceholderTextColorProperty, Value = UIColor.FromRGB(125, 125, 125)},
				new Setter {Property = Views.View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
				new Setter {Property = Views.View.CornerRadiusProperty, Value = 0.0},
				new Setter {Property = Views.View.PaddingProperty, Value = 5},
				new Setter {Property = Views.TextEntryView.VerticalTextAlignProperty, Value = UITextAlign.Middle}
			}
		};
	}
}