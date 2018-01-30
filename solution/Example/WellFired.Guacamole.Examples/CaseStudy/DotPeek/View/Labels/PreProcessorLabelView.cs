﻿using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels
{
	public class PreProcessorLabelView : LabelView
	{
		public PreProcessorLabelView()
		{
			Style = NoBackgroundNoOutline.Style;
			Bind(TextProperty, "PreProcessor", BindingMode.ReadOnly);
		}
	}
}