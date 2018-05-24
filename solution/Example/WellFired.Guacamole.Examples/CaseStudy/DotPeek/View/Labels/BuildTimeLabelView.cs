using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels
 {
 	public class BuildTimeLabelView : LabelView
 	{
		 public BuildTimeLabelView()
		 {
			 Style = NoBackgroundNoOutline.Style;
			 Bind(TextProperty, "BuildTime", BindingMode.ReadOnly);
		 }
 	}
 }