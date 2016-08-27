using WellFired.Guacamole.Types;
using WellFired.Guacamole.View;

namespace WellFired.Guacamole.Styling.Styles
{
	internal static class Slider
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter { Property = ViewBase.BackgroundColorProperty, Value = UIColor.FromRGB(255, 255, 255) },
				new Setter { Property = ViewBase.OutlineColorProperty, Value = UIColor.FromRGB(255, 255, 255) },

				new Setter { Property = ViewBase.HorizontalLayoutProperty, Value = LayoutOptions.Fill },
				new Setter { Property = ViewBase.VerticalLayoutProperty, Value = LayoutOptions.Expand },

				new Setter { Property = ViewBase.PaddingProperty, Value = 5 },
				new Setter { Property = ViewBase.CornerRadiusProperty, Value = 0.0 }
			}
		};
	}
}