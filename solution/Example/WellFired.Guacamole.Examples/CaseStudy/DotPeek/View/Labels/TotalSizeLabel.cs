using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels
{
	public class TotalSizeLabel : Label
	{
		public TotalSizeLabel()
		{
			Style = NoBackgroundNoOutline.Style;
			Bind(TextProperty, "TotalSize", BindingMode.ReadOnly);
		}
	}
}