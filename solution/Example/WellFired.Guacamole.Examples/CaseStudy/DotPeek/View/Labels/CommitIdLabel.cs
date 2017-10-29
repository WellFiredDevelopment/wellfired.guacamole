﻿using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels
{
	public class CommitIdLabel : Label
	{
		public CommitIdLabel()
		{
			Style = NoBackgroundNoOutline.Style;
			Bind(TextProperty, "CommitId", BindingMode.ReadOnly);
		}
	}
}