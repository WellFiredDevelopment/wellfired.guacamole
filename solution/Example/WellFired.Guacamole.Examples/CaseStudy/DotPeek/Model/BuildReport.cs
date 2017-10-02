using System.Collections.Generic;
using System.Text;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
	public class BuildReport
	{
		public BuildOverview BuildOverview = new BuildOverview();
		public BuildSettings BuildSettings = new BuildSettings();

		public List<IAsset> NonResourcesIncludedAssets = new List<IAsset>();
		public List<IAsset> ResourcesIncludedAssets = new List<IAsset>();
		public List<IAsset> UnusedAssets = new List<IAsset>();
		public List<IScene> BuildScenes = new List<IScene>();
		
		public static BuildReport Null { get; } = new BuildReport();

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();
            
			stringBuilder.Append("Build Overview :" + BuildOverview);
            
			stringBuilder.Append("Resources assets included in build :\n");
			foreach (var asset in ResourcesIncludedAssets)
				stringBuilder.Append($"{asset}\n");
			
			stringBuilder.Append("\nNon resources assets included in build :\n");
			foreach (var asset in NonResourcesIncludedAssets)
				stringBuilder.Append($"{asset}\n");

			stringBuilder.Append("\nUnused assets :\n");
			foreach (var asset in UnusedAssets)
				stringBuilder.Append($"{asset}\n");

			stringBuilder.Append("\nPreprocessors :\n");
			foreach (var preprocessor in BuildSettings.CompileDirectives)
				stringBuilder.Append($"{preprocessor}\n");

			stringBuilder.Append("\nBuild scenes :\n");
			foreach (var scene in BuildScenes)
				stringBuilder.Append($"{scene}\n");

			return stringBuilder.ToString();
		}
	}
}