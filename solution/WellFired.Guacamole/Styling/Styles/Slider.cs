using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
	internal static class Slider
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(125, 125, 125)},
				new Setter {Property = View.OutlineColorProperty, Value = UIColor.FromRGB(0, 0, 0)},
				new Setter {Property = View.OutlineThicknessProperty, Value = 4.0},
				new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
				new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
				new Setter {Property = View.PaddingProperty, Value = 5},
				new Setter {Property = View.CornerRadiusProperty, Value = 6.0},
				new Setter {Property = SliderView.ThumbBackgroundColorProperty, Value = UIColor.FromRGB(64, 124, 191)},
				new Setter {Property = SliderView.ThumbOutlineColorProperty, Value = UIColor.FromRGB(0, 0, 0)},
				new Setter {Property = SliderView.ThumbCornerRadiusProperty, Value = 0.0}
			},
			Triggers =
			{
				new Trigger
				{
					Property = View.ControlStateProperty,
					Value = ControlState.Normal,
					Setters =
					{
						new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(125, 125, 125)},
						new Setter {Property = SliderView.ThumbBackgroundColorProperty, Value = UIColor.FromRGB(125, 125, 125)}
					}
				},
				new Trigger
				{
					Property = View.ControlStateProperty,
					Value = ControlState.Hover,
					Setters =
					{
						new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(158, 158, 158)},
						new Setter {Property = SliderView.ThumbBackgroundColorProperty, Value = UIColor.FromRGB(158, 158, 158)}
					}
				},
				new Trigger
				{
					Property = View.ControlStateProperty,
					Value = ControlState.Active,
					Setters =
					{
						new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(64, 124, 191)},
						new Setter {Property = SliderView.ThumbBackgroundColorProperty, Value = UIColor.FromRGB(64, 124, 191)}
					}
				},
				new Trigger
				{
					Property = View.ControlStateProperty,
					Value = ControlState.Disabled,
					Setters =
					{
						new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGB(100, 100, 100)},
						new Setter {Property = SliderView.ThumbBackgroundColorProperty, Value = UIColor.FromRGB(100, 100, 100)}
					}
				}
			}
		};
	}
}