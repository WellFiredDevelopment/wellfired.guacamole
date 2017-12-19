using WellFired.Guacamole.Styling;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles
{
	internal static class Page
	{
		public static readonly Style Style = new Style
		{
			Setters =
			{
				new Setter {Property = Guacamole.Views.View.BackgroundColorProperty, Value = Shared.BackgroundColor}
			}
		};
	}
}