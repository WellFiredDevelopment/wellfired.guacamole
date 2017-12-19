using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels
{
	public class PlatformLabel : Label
	{
		public PlatformLabel()
		{
			Style = NoBackgroundNoOutline.Style;
			Bind(TextProperty, "Platform", BindingMode.ReadOnly);
		}
	}
}