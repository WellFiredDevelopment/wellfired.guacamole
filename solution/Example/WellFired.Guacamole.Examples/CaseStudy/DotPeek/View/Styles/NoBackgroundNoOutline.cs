using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles
{
	internal static class NoBackgroundNoOutline
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter {Property = Guacamole.Views.View.BackgroundColorProperty, Value = Shared.BackgroundColor},
				new Setter {Property = Guacamole.Views.View.OutlineColorProperty, Value = Shared.BackgroundColor},
				new Setter {Property = Guacamole.Views.View.CornerRadiusProperty, Value = 0.0},
				new Setter {Property = Guacamole.Views.View.OutlineThicknessProperty, Value = 0.0}
			}
		};
	}
}