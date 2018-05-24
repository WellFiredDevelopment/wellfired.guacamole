using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
	internal static class ButtonView
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGBA(125, 125, 125, 255)},
				new Setter {Property = View.OutlineColorProperty, Value = UIColor.FromRGB(88, 88, 88)},
				new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
				new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
				new Setter {Property = View.PaddingProperty, Value = 5},
				new Setter {Property = Views.ButtonView.TextColorProperty, Value = UIColor.White},
				new Setter {Property = View.CornerRadiusProperty, Value = 8.0},
				new Setter {Property = View.OutlineThicknessProperty, Value = 6.0},
				new Setter {Property = Views.ButtonView.HorizontalTextAlignProperty, Value = UITextAlign.Middle},
				new Setter {Property = Views.ButtonView.VerticalTextAlignProperty, Value = UITextAlign.Middle}
			},
			Triggers =
			{
				new Trigger
				{
					Property = View.ControlStateProperty,
					Value = ControlState.Normal,
					Setters =
					{
						new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(125, 125, 125)}
					}
				},
				new Trigger
				{
					Property = View.ControlStateProperty,
					Value = ControlState.Hover,
					Setters =
					{
						new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(158, 158, 158)}
					}
				},
				new Trigger
				{
					Property = View.ControlStateProperty,
					Value = ControlState.Active,
					Setters =
					{
						new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(64, 124, 191)}
					}
				},
				new Trigger
				{
					Property = View.ControlStateProperty,
					Value = ControlState.Disabled,
					Setters =
					{
						new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(100, 100, 100)}
					}
				}
			}
		};
	}
}