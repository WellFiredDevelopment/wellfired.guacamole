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
				new Setter {Property = View.BackgroundColorProperty, Value = UIColor.White},
				new Setter {Property = View.OutlineThicknessProperty, Value = 0},
				new Setter {Property = View.HorizontalLayoutProperty, Value = LayoutOptions.Fill},
				new Setter {Property = View.VerticalLayoutProperty, Value = LayoutOptions.Expand},
				new Setter {Property = LabelView.TextColorProperty, Value = UIColor.Black},
				new Setter {Property = LabelView.HorizontalTextAlignProperty, Value = UITextAlign.Start},
				new Setter {Property = LabelView.VerticalTextAlignProperty, Value = UITextAlign.Middle}
			},
			Triggers =
			{
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