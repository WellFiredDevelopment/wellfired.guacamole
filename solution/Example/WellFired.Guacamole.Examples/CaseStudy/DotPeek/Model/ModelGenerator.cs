using System;
using System.Collections.Generic;
using WellFired.Guacamole.Examples.CaseStudy.DotPeek.Model.Assets;

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
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Meshes, new FileSize(450),
                            4f)
                    }),
                },
                
                ResourcesIncludedAssets = new List<IAsset>(new IAsset[]
                {
                    new Model3D
                    {
                        ImportedSize = new FileSize(456),
                        RawSize = new FileSize(477),
                        Path = "Assets/Resources/Meshes/bebe.fbx",
                        Percentage = 0.2f
                    },
                    new Model3D()
                    {
                        ImportedSize = new FileSize(1024),
                        RawSize = new FileSize(1024),
                        Path = "Assets/Resources/Prefab/mayi.prefab",
                        Percentage = 2f
                    }
                })
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
                },

                ResourcesIncludedAssets = new List<IAsset>(new IAsset[]
                {
                    new Model3D
                    {
                        ImportedSize = new FileSize(456),
                        RawSize = new FileSize(477),
                        Path = "Assets/Meshes/bebe.fbx",
                        Percentage = 0.2f
                    },
                    new Prefab()
                    {
                        ImportedSize = new FileSize(512),
                        RawSize = new FileSize(512),
                        Path = "Assets/Meshes/mayi.prefab",
                        Percentage = 2f
                    }
                })
            };
        }
    }
}