using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels
{
	public class TotalSizeLabelView : LabelView
	{
		public TotalSizeLabelView()
		{
			Style = NoBackgroundNoOutline.Style;
			Bind(TextProperty, "TotalSize", BindingMode.ReadOnly);
		}
	}
}