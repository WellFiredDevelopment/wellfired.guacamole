using WellFired.Guacamole.DataBinding;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Styles;
using WellFired.Guacamole.Views;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.View.Labels
{
	public class ResourceAssetSizeLabelView : LabelView
	{
		public ResourceAssetSizeLabelView()
		{
			Style = NoBackgroundNoOutline.Style;
			Bind(TextProperty, "ResourceAssetsSize", BindingMode.ReadOnly);
			Bind(BackgroundColorProperty, "ResourceAssetsSizeBackgroundColor", BindingMode.ReadOnly);
		}
	}
}