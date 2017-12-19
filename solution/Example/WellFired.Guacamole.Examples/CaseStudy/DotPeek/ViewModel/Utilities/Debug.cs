using System;
using System.Collections.Generic;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.ViewModel.Utilities
{
	public static class Utility
	{
		public static BuildReport GenerateTempBuildReport()
		{
			return new BuildReport
			{
				BuildOverview = new BuildOverview
				{
					CommitId = "a.a.a",
					BuildSize = new FileSize(1024),
					BuildTime = DateTime.Now,
					Platform = "Test",
					UnityVersion = "Good",
					BuildAssetSplits =
					{
						new BuildOverview.BuildAssetSplit(BuildOverview.Category.Animations, new FileSize(1024), 0.3f),
						new BuildOverview.BuildAssetSplit(BuildOverview.Category.FileHeaders, new FileSize(1024), 0.7f),
						new BuildOverview.BuildAssetSplit(BuildOverview.Category.OtherAssets, new FileSize(1024), 0.7f),
						new BuildOverview.BuildAssetSplit(BuildOverview.Category.Levels, new FileSize(1024), 0.7f),
						new BuildOverview.BuildAssetSplit(BuildOverview.Category.Shaders, new FileSize(1024), 0.7f),
						new BuildOverview.BuildAssetSplit(BuildOverview.Category.Sounds, new FileSize(1024), 0.7f)
					}
				},
				BuildSettings = new BuildSettings
				{
					CompileDirectives = new List<string> {"Test", "Another"},
					PreprocessorOrigin = PreprocessorOrigin.Editor
				},
				NonResourcesIncludedAssets = new List<IAsset> {new Anim {Path = "a/test/thin", ImportedSize = new FileSize(1024), RawSize = new FileSize(1024), Percentage = 0.7f}},
				ResourcesIncludedAssets = new List<IAsset> {new Anim {Path = "a/test/thin", ImportedSize = new FileSize(1024), RawSize = new FileSize(1024), Percentage = 0.7f}},
				UnusedAssets = new List<IAsset> {new Anim {Path = "a/test/thin", ImportedSize = new FileSize(1024), RawSize = new FileSize(1024), Percentage = 0.7f}},
				BuildScenes = new List<IScene> {new Scene {Path = "a/test/thin", ImportedSize = new FileSize(1024), RawSize = new FileSize(1024), Percentage = 0.7f}},
			};
		}

		public static List<IAsset> ListOfAssets()
		{
			return new List<IAsset> { new Anim { Path = "a/test/thin", ImportedSize = new FileSize(1024), RawSize = new FileSize(1024), Percentage = 0.7f} };
		}
	}
}