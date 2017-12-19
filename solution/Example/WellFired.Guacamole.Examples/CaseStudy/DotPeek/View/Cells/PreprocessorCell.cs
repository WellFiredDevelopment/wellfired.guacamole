using WellFired.Guacamole.Cells;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Cells
{
	public class PreProcessorCell : Cell
	{
		public PreProcessorCell()
		{
			Style = NoBackgroundNoOutline.Style;
			Content = new PreProcessorLabel();
		}
	}
}