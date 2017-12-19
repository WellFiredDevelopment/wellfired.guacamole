using System.Collections.Generic;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
	public class UnusedAssets : AssetsBase
	{
		public UnusedAssets(List<IAsset> assets, List<IAsset> previousAssets) : base(assets, previousAssets)
		{
		}
	}
}