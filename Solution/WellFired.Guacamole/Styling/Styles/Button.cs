using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Styling.Styles
{
	internal static class Button
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter {Property = ViewBase.BackgroundColorProperty, Value = UIColor.FromRGBA(125, 125, 125, 255)},
				new Setter {Property = ViewBase.OutlineColorProperty, Value = UIColor.FromRGB(88, 88, 88)},
				new Setter {Property = ViewBase.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
				new Setter {Property = ViewBase.VerticalLayoutProperty, Value = LayoutOptions.Expand},
				new Setter {Property = ViewBase.PaddingProperty, Value = 5},
				new Setter {Property = View.Button.TextColorProperty, Value = UIColor.White},
				new Setter {Property = ViewBase.CornerRadiusProperty, Value = 8.0},
				new Setter {Property = View.Button.HorizontalTextAlignProperty, Value = UITextAlign.Middle},
				new Setter {Property = View.Button.VerticalTextAlignProperty, Value = UITextAlign.Middle}
			},
			Triggers =
			{
				new Trigger
				{
					Property = ViewBase.ControlStateProperty,
					Value = ControlState.Normal,
					Setters =
					{
						new Setter {Property = ViewBase.BackgroundColorProperty, Value = UIColor.FromRGB(125, 125, 125)}
					}
				},
				new Trigger
				{
					Property = ViewBase.ControlStateProperty,
					Value = ControlState.Hover,
					Setters =
					{
						new Setter {Property = ViewBase.BackgroundColorProperty, Value = UIColor.FromRGB(158, 158, 158)}
					}
				},
				new Trigger
				{
					Property = ViewBase.ControlStateProperty,
					Value = ControlState.Active,
					Setters =
					{
						new Setter {Property = ViewBase.BackgroundColorProperty, Value = UIColor.FromRGB(64, 124, 191)}
					}
				},
				new Trigger
				{
					Property = ViewBase.ControlStateProperty,
					Value = ControlState.Disabled,
					Setters =
					{
						new Setter {Property = ViewBase.BackgroundColorProperty, Value = UIColor.FromRGB(100, 100, 100)}
					}
				}
			}
		};
	}
}