using WellFired.Guacamole.Data;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Styling.Styles
{
	internal static class Label
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter {Property = View.BackgroundColorProperty, Value = UIColor.FromRGBA(125, 125, 125, 255)},
				new Setter {Property = View.OutlineColorProperty, Value = UIColor.FromRGB(88, 88, 88)},
				new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
				new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
				new Setter {Property = Views.Label.TextColorProperty, Value = UIColor.White},
				new Setter {Property = Views.Label.HorizontalTextAlignProperty, Value = UITextAlign.Middle},
				new Setter {Property = Views.Label.VerticalTextAlignProperty, Value = UITextAlign.Middle}
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