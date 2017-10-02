using System.Collections.Generic;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel
{
    public class UsedAssets : AssetsBase
    {
        public UsedAssets(List<IAsset> assets, List<IAsset> previousAssets) : base(assets, previousAssets)
        {
        }
    }
}