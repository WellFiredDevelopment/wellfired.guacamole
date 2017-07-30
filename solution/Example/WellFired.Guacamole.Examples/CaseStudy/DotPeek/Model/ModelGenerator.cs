using System;
using System.Collections.Generic;

namespace WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model
{
    public static class ModelGenerator
    {
        public static BuildReport GetCurrentReport()
        {
            return new BuildReport
            {
                BuildOverview = new BuildOverview()
                {
                    BuildTime = new DateTime(2017, 7, 30, 14, 50, 23),
                    CommitID = "af32huh",
                    Platform = "Windows Standalone",
                    UnityVersion = "5.5.1f1",
                    BuildSize = new FileSize(2024),
                    BuildAssetSplits = new List<BuildOverview.BuildAssetSplit>(new[]
                    {
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Animations, new FileSize(259),
                            2.4f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Meshes, new FileSize(280),
                            4f)
                    })
                }
            };
        }
        
        public static BuildReport GetPreviousReport()
        {
            return new BuildReport
            {
                BuildOverview = new BuildOverview()
                {
                    BuildTime = new DateTime(2017, 7, 29, 13, 50, 23),
                    CommitID = "adff4huh",
                    Platform = "Windows Standalone",
                    UnityVersion = "5.5.2f3",
                    BuildSize = new FileSize(1068),
                    BuildAssetSplits = new List<BuildOverview.BuildAssetSplit>(new[]
                    {
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Animations, new FileSize(259),
                            2.4f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Meshes, new FileSize(300),
                            3f)
                    })
                }
            };
        }
    }
}