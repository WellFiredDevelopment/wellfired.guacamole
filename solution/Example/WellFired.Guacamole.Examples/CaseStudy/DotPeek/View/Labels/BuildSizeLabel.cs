using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels
{
	public class BuildSizeLabel : Label
	{
		public BuildSizeLabel()
		{
			Style = NoBackgroundNoOutline.Style;
			Bind(TextProperty, "BuildSize", BindingMode.ReadOnly);
		}
	}
}