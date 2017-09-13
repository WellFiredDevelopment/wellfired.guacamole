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
                            4f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.IncludedDLLs, new FileSize(1340),
                            19.2f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Sounds, new FileSize(3590),
                            15f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Textures, new FileSize(54895),
                            22.6f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.OtherAssets, new FileSize(289),
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
                    new Prefab()
                    {
                        ImportedSize = new FileSize(1024),
                        RawSize = new FileSize(1024),
                        Path = "Assets/Resources/Prefab/mayi.prefab",
                        Percentage = 2f
                    }
                }),
                
                NonResourcesIncludedAssets = new List<IAsset>(new IAsset[]
                { 
                    new Model3D
                    {
                        ImportedSize = new FileSize(1203),
                        RawSize = new FileSize(1596),
                        Path = "Assets/Meshes/gorilla.fbx",
                        Percentage = 0.2f
                    },
                    new Prefab()
                    {
                        ImportedSize = new FileSize(512),
                        RawSize = new FileSize(512),
                        Path = "Assets/Prefab/trotro.prefab",
                        Percentage = 2f
                    }
                }),
                
                UnusedAssets = new List<IAsset>(new IAsset[]
                { 
                    new Model3D
                    {
                        ImportedSize = new FileSize(1203),
                        RawSize = new FileSize(1596),
                        Path = "Assets/Meshes/sichuanpepper.fbx",
                        Percentage = 0.2f
                    },
                    new Prefab()
                    {
                        ImportedSize = new FileSize(1524),
                        RawSize = new FileSize(1524),
                        Path = "Assets/Prefab/blues.prefab",
                        Percentage = 2f
                    }
                }),
                
                Preprocessors = new List<string>(new []{"UNITY_IOS", "VUFORIA", "UNITY_5_5_1f", "DEBUG"})
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
                            3f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.IncludedDLLs, new FileSize(1340),
                            19.2f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Sounds, new FileSize(3590),
                            13f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.Textures, new FileSize(54895),
                            22.6f),
                        new BuildOverview.BuildAssetSplit(BuildOverview.Category.OtherAssets, new FileSize(289),
                            4f)
                    })
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
                    new Prefab()
                    {
                        ImportedSize = new FileSize(512),
                        RawSize = new FileSize(512),
                        Path = "Assets/Resources/Prefab/mayi.prefab",
                        Percentage = 2f
                    }
                }),
                
                NonResourcesIncludedAssets = new List<IAsset>(new IAsset[]
                { 
                    new Model3D
                    {
                        ImportedSize = new FileSize(512),
                        RawSize = new FileSize(1596),
                        Path = "Assets/Meshes/gorilla.fbx",
                        Percentage = 0.2f
                    },
                    new Prefab()
                    {
                        ImportedSize = new FileSize(512),
                        RawSize = new FileSize(512),
                        Path = "Assets/Prefab/trotro.prefab",
                        Percentage = 2f
                    }
                }),
                
                UnusedAssets = new List<IAsset>(new IAsset[]
                { 
                    new Model3D
                    {
                        ImportedSize = new FileSize(1500),
                        RawSize = new FileSize(2000),
                        Path = "Assets/Meshes/sichuanpepper.fbx",
                        Percentage = 0.2f
                    }
                }),
                
                Preprocessors = new List<string>(new []{"UNITY_IOS", "VUFORIA", "UNITY_5_5_1f", "TEST_ACTIVATED"})
            };
        }
    }
}