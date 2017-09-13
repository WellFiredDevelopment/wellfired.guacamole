using System.Collections.Generic;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
    public class BuildReport
    {
        public BuildOverview BuildOverview;
        
        public List<IAsset> NonResourcesIncludedAssets = new List<IAsset>();
        public List<IAsset> ResourcesIncludedAssets = new List<IAsset>();
        public List<IAsset> UnusedAssets = new List<IAsset>();
        
        public List<string> Preprocessors = new List<string>();
    }
}